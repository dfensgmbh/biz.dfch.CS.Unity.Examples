/**
 * Copyright 2021 d-fens GmbH
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Unity;

namespace Assets.Managers
{
    public class UnityContainerManager
    {
        // ReSharper disable once InconsistentNaming
        protected static IUnityContainer _container;

        public static IUnityContainer Container
        {
            get => _container;
            set => _container = value;
        }

        static UnityContainerManager()
        {
            if (_container == null)
            {
                Container = new UnityContainer();
            }
        }

        public static T Resolve<T>()
        {
            T result = default(T);
            if (Container.IsRegistered(typeof(T)))
            {
                result = Container.Resolve<T>();
            }
            return result;
        }

        public static void RegisterInstance<T>(T instance) 
        {
            if (!Container.IsRegistered<T>())
            {
                Container.RegisterInstance(instance);
            }
        }
    }
}
