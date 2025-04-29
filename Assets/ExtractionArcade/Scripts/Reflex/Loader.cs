using Plugins.MonoCache;
using Reflex.Core;
using UnityEngine.AddressableAssets;

namespace ExtractionArcade.Scripts.Reflex
{
    public class Loader : MonoCache
    {
        private void Start()
        {
            Addressables.LoadSceneAsync(Constants.MAIN_SCENE, activateOnLoad: false)
                .Completed += handle =>
            {
                ReflexSceneManager.PreInstallScene(handle.Result.Scene, builder => builder.AddSingleton(Constants.MAIN_SCENE));
                handle.Result.ActivateAsync();
            };
        }
    }
}