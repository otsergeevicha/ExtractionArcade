using Plugins.MonoCache;
using Reflex.Core;
using UnityEngine.AddressableAssets;

namespace ExtractionArcade.Scripts.Reflex
{
    public class Loader : MonoCache
    {
        private void Start()
        {
            Addressables.LoadSceneAsync(Constants.MainScene, activateOnLoad: false)
                .Completed += handle =>
            {
                ReflexSceneManager.PreInstallScene(handle.Result.Scene, builder => builder.AddSingleton(Constants.MainScene));
                handle.Result.ActivateAsync();
            };
        }
    }
}