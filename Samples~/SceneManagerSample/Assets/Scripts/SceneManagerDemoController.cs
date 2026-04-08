using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameplayMechanicsUMFOSS.Systems;

namespace GameplayMechanicsUMFOSS.Samples.SceneManagerSample
{
    /// <summary>
    /// Wires the demo scene buttons and on-screen labels to SceneManager_UMFOSS.
    /// Drop this on a Canvas in the PersistentScene's UI and assign references.
    /// </summary>
    public class SceneManagerDemoController : MonoBehaviour
    {
        [Header("Scene Names (must match Build Settings)")]
        [SerializeField] private string sceneA = "SceneA";
        [SerializeField] private string sceneB = "SceneB";
        [SerializeField] private string pauseMenuScene = "PauseMenuScene";

        [Header("Transitions")]
        [SerializeField] private SceneTransition_UMFOSS instantTransition;
        [SerializeField] private SceneTransition_UMFOSS fadeBlackTransition;
        [SerializeField] private SceneTransition_UMFOSS fadeWhiteTransition;
        [SerializeField] private SceneTransition_UMFOSS loadingBarTransition;

        [Header("Buttons")]
        [SerializeField] private Button loadSceneBButton;
        [SerializeField] private Button loadSceneAButton;
        [SerializeField] private Button pushPauseMenuButton;
        [SerializeField] private Button popPauseMenuButton;
        [SerializeField] private Button reloadCurrentButton;
        [SerializeField] private Button loadSceneBWithBarButton;

        [Header("HUD Labels")]
        [SerializeField] private TextMeshProUGUI currentSceneLabel;
        [SerializeField] private TextMeshProUGUI stackDepthLabel;
        [SerializeField] private TextMeshProUGUI isTransitioningLabel;
        [SerializeField] private TextMeshProUGUI persistentCounterLabel;

        // The persistent counter proves the persistent scene survives all loads.
        // It only ever increases — if you ever see it reset, the persistent scene was unloaded.
        private float persistentCounter;

        private void Start()
        {
            if (loadSceneBButton != null)
                loadSceneBButton.onClick.AddListener(() => SceneManager_UMFOSS.Instance.LoadScene(sceneB, fadeBlackTransition));

            if (loadSceneAButton != null)
                loadSceneAButton.onClick.AddListener(() => SceneManager_UMFOSS.Instance.LoadScene(sceneA, fadeWhiteTransition));

            if (pushPauseMenuButton != null)
                pushPauseMenuButton.onClick.AddListener(() => SceneManager_UMFOSS.Instance.Push(pauseMenuScene, instantTransition));

            if (popPauseMenuButton != null)
                popPauseMenuButton.onClick.AddListener(() => SceneManager_UMFOSS.Instance.Pop(instantTransition));

            if (reloadCurrentButton != null)
                reloadCurrentButton.onClick.AddListener(() => SceneManager_UMFOSS.Instance.ReloadScene(fadeBlackTransition));

            if (loadSceneBWithBarButton != null)
                loadSceneBWithBarButton.onClick.AddListener(() => SceneManager_UMFOSS.Instance.LoadScene(sceneB, loadingBarTransition));
        }

        private void Update()
        {
            persistentCounter += Time.unscaledDeltaTime;

            var sm = SceneManager_UMFOSS.Instance;
            if (sm == null) return;

            if (currentSceneLabel != null)
                currentSceneLabel.text = $"Current Scene: {sm.GetCurrentScene()}";

            if (stackDepthLabel != null)
                stackDepthLabel.text = $"Stack Depth: {sm.GetStackDepth()}";

            if (isTransitioningLabel != null)
                isTransitioningLabel.text = $"Transitioning: {sm.IsTransitioning()}";

            if (persistentCounterLabel != null)
                persistentCounterLabel.text = $"Persistent Counter: {persistentCounter:F1}s";
        }
    }
}
