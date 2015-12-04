using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace TaskManager.Helper.ModelBinder
{
    public class JsonModelBinderContainer
    {
        private static readonly object SyncObj = new object();
        private readonly Dictionary<Type, IModelBinder> _dic;

        private static JsonModelBinderContainer _instance;
        public static JsonModelBinderContainer Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new JsonModelBinderContainer();
                        }
                    }
                }
                return _instance;
            }
        }

        private JsonModelBinderContainer()
        {
            _dic = new Dictionary<Type, IModelBinder>();
            Init();
        }

        private void Init()
        {
            _dic.Add(typeof(List<string>), new JsonModelBinder<List<string>>());

            InitLibraryModel();
        }

        private void InitLibraryModel()
        {
        }


        public IModelBinder ResolveModelBinder(Type t)
        {
            if (!_dic.ContainsKey(t))
            {
                throw new ArgumentException("****************can't find Type");
            }
            return _dic[t];
        }
    }
}