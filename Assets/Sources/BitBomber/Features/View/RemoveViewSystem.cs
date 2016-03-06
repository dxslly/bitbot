using Entitas;

namespace BitBots.BitBomber.Features.View
{
    public class RemoveViewSystem : ISetPool, ISystem
    {
        private Group _views;

        public void SetPool(Pool pool)
        {
            _views = pool.GetGroup(CoreMatcher.View);
            _views.OnEntityUpdated += OnViewReplaced;
            _views.OnEntityRemoved += OnViewRemoved;
        }

        private void OnViewRemoved(Group group, Entity entity, int index, IComponent component)
        {
            ViewComponent view = (ViewComponent)component;
            if (null == view.gameObject)
            {
                return;
            }

            UnityEngine.Object.Destroy(view.gameObject);
        }

        private void OnViewReplaced(Group group, Entity entity, int index, IComponent previousComponent, IComponent newComponent)
        {
            // Destroy previous View GameObject
            ViewComponent view = (ViewComponent)previousComponent;
            if (null == view.gameObject)
            {
                return;
            }

            UnityEngine.Object.Destroy(view.gameObject);
        }
    }
}