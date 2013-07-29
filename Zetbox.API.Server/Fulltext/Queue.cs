using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zetbox.API.Server.Fulltext
{
    public class IndexUpdate
    {
        public class Text
        {
            public Text()
            {
            }

            public Text(string body)
            {
                this.Body = body;
            }

            public string Body { get; set; }

            public Dictionary<string, string> Fields { get; set; }
        }

        public static readonly List<Tuple<InterfaceType, int, Text>> NothingAdded = new List<Tuple<InterfaceType, int, Text>>();
        public static readonly List<Tuple<InterfaceType, int, Text>> NothingModified = new List<Tuple<InterfaceType, int, Text>>();
        public static readonly List<Tuple<InterfaceType, int>> NothingDeleted = new List<Tuple<InterfaceType, int>>();

        public List<Tuple<InterfaceType, int, Text>> added;
        public List<Tuple<InterfaceType, int, Text>> modified;
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
                if (item.added == null) item.added = IndexUpdate.NothingAdded;
                if (item.modified == null) item.modified = IndexUpdate.NothingModified;
                if (item.deleted == null) item.deleted = IndexUpdate.NothingDeleted;

                _service.Enqueue(item);
            }
        }
    }
}
