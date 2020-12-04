namespace MVC
{
    public abstract class ControllerValueBase<TModel, TView> : ControllerBase<TView>
        where TModel : struct
        where TView : class
    {
        protected TModel _model;

        public void LoadModel(TModel model)
        {
            InternalLoadModel(model);
            _model = model;
        }

        public void UnloadModel()
        {
            InternalUnloadModel(_model);
            _model = default;
        }

        protected virtual void InternalLoadModel(TModel model) { }

        protected virtual void InternalUnloadModel(TModel model) { }
    }

    public abstract class ControllerReferenceBase<TModel, TView> : ControllerBase<TView>
        where TModel : class
        where TView : class
    {
        protected TModel _model;

        public void LoadModel(TModel model)
        {
            UnloadModel();

            if (model == null)
                return;

            InternalLoadModel(model);
            _model = model;
        }

        public void UnloadModel()
        {
            if (_model == null)
                return;

            InternalUnloadModel(_model);
            _model = null;
        }

        protected virtual void InternalLoadModel(TModel model) { }

        protected virtual void InternalUnloadModel(TModel model) { }
    }

    public abstract class ControllerBase<TView>
        where TView : class
    {
        protected TView _view;

        public void ConnectView(TView view)
        {
            DisconnectView();

            if (view == null)
                return;

            InternalConnectView(view);
            _view = view;
        }

        public void DisconnectView()
        {
            if (_view == null)
                return;

            InternalDisconnectView(_view);
            _view = null;
        }

        protected virtual void InternalConnectView(TView view) { }

        protected virtual void InternalDisconnectView(TView view) { }
    }
}