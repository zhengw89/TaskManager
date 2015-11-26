using System.Collections.Generic;

namespace TaskManager.Repository.Factory.Register
{
    internal abstract class BaseRepositoryContainerRegister
    {
        private delegate void RegisterSubProcess(IRepositoryContainer container);
        private readonly List<RegisterSubProcess> _registerSubProcessesChain;

        protected BaseRepositoryContainerRegister()
        {
            this._registerSubProcessesChain = new List<RegisterSubProcess>();
            this.RegistSubProcessChain();
        }

        public void Register(IRepositoryContainer container)
        {
            foreach (var registerSubProcess in _registerSubProcessesChain)
            {
                registerSubProcess.Invoke(container);
            }
        }

        #region Register method

        protected abstract void RegisterDevRepositories(IRepositoryContainer container);
        protected abstract void RegisterOrgRepositories(IRepositoryContainer container);
        protected abstract void RegisterTaRepositories(IRepositoryContainer container);
        protected abstract void RegisterUbRepositories(IRepositoryContainer container);

        #endregion

        private void RegistSubProcessChain()
        {
            this._registerSubProcessesChain.Add(RegisterDevRepositories);
            this._registerSubProcessesChain.Add(RegisterOrgRepositories);
            this._registerSubProcessesChain.Add(RegisterTaRepositories);
            this._registerSubProcessesChain.Add(RegisterUbRepositories);
        }
    }
}
