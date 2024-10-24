using UnityEngine;

namespace DI
{
    public class DIExampleScene : MonoBehaviour
    {
        public void Init(DIContainer projectContainer)
        {
            /*var serviceWithoutTag = projectContainer.Resolve<MyAwesomeProjectService>();*/

            var sceneContainer = new DIContainer(projectContainer);
            sceneContainer.RegisterSingleton(c => new MySceneService(c.Resolve<MyAwesomeProjectService>()));
        }
    }
}