using Game.Core.DI;
using Game.Data;
using Game.Gameplay.Core;
using Game.Gameplay.Features.Input;
using Game.Gameplay.Features.MainHero;
using Game.Gameplay.Features.StagesFeature;
using Game.Meta.Features.LevelsProgression;
using Game.UI.Gameplay;
using Game.Utility.Conditions;
using Game.Utility.CoroutineManagement;
using Game.Utility.SceneManagement;

namespace Game.Gameplay.States
{
    public class GameplayStatesFactory
    {
        private readonly DIContainer _container;

        public GameplayStatesFactory(DIContainer container)
        {
            _container = container;
        }

        public PreparationState CreatePreparationState()
        {
            return new PreparationState(_container.Resolve<PreparationTriggerService>());
        }

        public StageProcessStage CreateStageProcessStage()
        {
            return new StageProcessStage(_container.Resolve<StageProviderService>());
        }

        public WinState CreateWinState(GameplayInputArgs inputArgs)
        {
            return new WinState(
                _container.Resolve<IInputService>(),
                _container.Resolve<LevelsProgressionService>(),
                inputArgs,
                _container.Resolve<PlayerDataProvider>(),
                _container.Resolve<ICoroutineRunner>(),
            _container.Resolve<GameplayPopupService>());
        }
        
        public DefeatState CreateDefeatState()
        {
            return new DefeatState(
                _container.Resolve<IInputService>(),
                _container.Resolve<GameplayPopupService>());
        }

        public GameplayStateMachine CreateGameStateMachine(GameplayInputArgs inputArgs)
        {
            var preparationTriggerService = _container.Resolve<PreparationTriggerService>();
            var stageProviderService = _container.Resolve<StageProviderService>();
            var mainHeroHolderService = _container.Resolve<MainHeroHolderService>();
            
            GameplayStateMachine coreLoopState = CreateCoreLoopState();

            DefeatState defeatState = CreateDefeatState();
            WinState winState = CreateWinState(inputArgs);

            ICompositeCondition coreLoopToWinStateCondition = new CompositeCondition()
                .Add(new FuncCondition(() => preparationTriggerService.HasMainHeroContact.Value))
                .Add(new FuncCondition(() => stageProviderService.CurrentStageResult.Value == StageResults.Completed))
                .Add(new FuncCondition(() => stageProviderService.HasNextStage() == false));

            ICompositeCondition coreLoopToDefeatStateCondition = new CompositeCondition()
                .Add(new FuncCondition(() =>
                {
                    if (mainHeroHolderService.MainHero != null)
                        return mainHeroHolderService.MainHero.IsDead.Value;

                    return false;
                }));

            GameplayStateMachine gameplayCycle = new GameplayStateMachine();
            
            gameplayCycle.AddState(coreLoopState);
            gameplayCycle.AddState(winState);
            gameplayCycle.AddState(defeatState);
            
            gameplayCycle.AddTransition(coreLoopState, winState, coreLoopToWinStateCondition);
            gameplayCycle.AddTransition(coreLoopState, defeatState, coreLoopToDefeatStateCondition);
            
            return gameplayCycle;
        }
        
        public GameplayStateMachine CreateCoreLoopState()
        {
            var preparationTriggerService = _container.Resolve<PreparationTriggerService>();
            var stageProviderService = _container.Resolve<StageProviderService>();
            
            var preparationState = CreatePreparationState();
            var stageProcessStage = CreateStageProcessStage();

            ICompositeCondition preparationToStateProcessCondition = new CompositeCondition()
                .Add(new FuncCondition(() => preparationTriggerService.HasMainHeroContact.Value))
                .Add(new FuncCondition(() => stageProviderService.HasNextStage()));
            
            FuncCondition stageProcessToPreparationCondition = 
                new FuncCondition(() => stageProviderService.CurrentStageResult.Value == StageResults.Completed);

            GameplayStateMachine coreLoopState = new GameplayStateMachine();
            
            coreLoopState.AddState(preparationState);
            coreLoopState.AddState(stageProcessStage);
            
            coreLoopState.AddTransition(preparationState, stageProcessStage, preparationToStateProcessCondition);
            coreLoopState.AddTransition(stageProcessStage, preparationState, stageProcessToPreparationCondition);
            
            return coreLoopState;
        }
    }
}