using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zetbox.API.Server.Fulltext
{
    public struct IndexUpdate
    {
        public List<Tuple<InterfaceType, int, string>> added;
        public List<Tuple<InterfaceType, int, string>> modified;
        public List<Tuple<InterfaceType, int>> deleted;

        public bool IsValid { get { return added != null && modified != null && deleted != null; } }
        public bool IsEmpty
        {
            get
            {
                return (added == null || added.Count == 0)
                    && (modified == null || modified.Count == 0)
                    && (deleted == null || deleted.Count == 0);
            }
        }
    }

    public interface IQueue
    {
        void Enqueue(IndexUpdate item);
    }

    internal class ServiceQueue : IQueue
    {
        private readonly Service _service;

        internal ServiceQueue(Service service = null)
        {
            _service = service;
        }

        public void Enqueue(IndexUpdate item)
        {
            if (_service != null)
            {
                if (item.added == null) item.added = new List<Tuple<InterfaceType, int, string>>();
                if (item.modified == null) item.modified = new List<Tuple<InterfaceType, int, string>>();
                if (item.deleted == null) item.deleted = new List<Tuple<InterfaceType, int>>();

                _service.Enqueue(item);
            }
        }
    }
}
