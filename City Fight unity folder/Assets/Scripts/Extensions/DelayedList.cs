using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Banagine.EC
{
    public class DelayedList<TGeneric> : IEnumerable<TGeneric>
    {
        public event Action<TGeneric> ItemAdded;
        public event Action<TGeneric> ItemRemoved;
        public event Action<List<TGeneric>> AllItemsRemoved;
        public event Action<List<TGeneric>> AllItemsAdded;

        public int Count
        {
            get
            {
                return _items.Count;
            }
        }

        private List<TGeneric> _itemsToAdd;
        private List<TGeneric> _itemsToRemove;
        private List<TGeneric> _items; 

        public DelayedList()
        {
            _itemsToAdd = new List<TGeneric>();
            _itemsToRemove = new List<TGeneric>();
            _items = new List<TGeneric>();
        } 

        public void Add(TGeneric item)
        {
            _itemsToAdd.Add(item);
        }

        public void Remove(TGeneric item)
        {
            _itemsToRemove.Add(item);
        }

        public void Remove<T>() where T : TGeneric
        {
            _itemsToRemove.Add(_items.Find(item => item.GetType() == typeof(T)));
        }

        public void RemoveItems<T>() where T : TGeneric
        {
            _itemsToRemove.AddRange(_items.FindAll(item => item.GetType() == typeof (T)));
        }

        public void RemoveAll()
        {
            _itemsToRemove.AddRange(_items);
        }

        public T GetItem<T>() where T : TGeneric
        {
            T itemToFind = (T)_items.Find(item => item is T);
            if (itemToFind == null)
            {
                itemToFind = (T)_itemsToAdd.Find(item => item is T);
            }
            return itemToFind;
        }

        public List<T> GetItems<T>() where T : TGeneric
        {
            List<T> itemsToFind = _items.FindAll(item => item is T).Cast<T>().ToList();
            if (itemsToFind.Count <= 0)
            {
                itemsToFind = _itemsToAdd.FindAll(item => item is T).Cast<T>().ToList();
            }
            return itemsToFind;
        }

        public TGeneric GetItemAtIndex(int index)
        {
            return _items[index];
        }

        public TGeneric Find(Predicate<TGeneric> predicate)
        {
            return _items.Find(predicate);
        }

        public List<TGeneric> FindAll(Predicate<TGeneric> predicate)
        {
            return _items.FindAll(predicate);
        } 

        public void UpdateList()
        {
            foreach (var itemToRemove in _itemsToRemove)
            {
                if (ItemRemoved != null)
                {
                    ItemRemoved.Invoke(itemToRemove);
                }
                _items.Remove(itemToRemove);
            }
            
            List<TGeneric> itemsRemoved = new List<TGeneric>();
            itemsRemoved.AddRange(_itemsToRemove);
            _itemsToRemove.Clear();
            if (AllItemsRemoved != null)
            {
                AllItemsRemoved.Invoke(itemsRemoved);
            }

            foreach (var itemToAdd in _itemsToAdd)
            {
                if (ItemAdded != null)
                {
                    ItemAdded.Invoke(itemToAdd);
                }
                
                _items.Add(itemToAdd);
            }
            
            List<TGeneric> itemsAdded = new List<TGeneric>();
            itemsAdded.AddRange(_itemsToAdd);
            _itemsToAdd.Clear();

            if (AllItemsAdded != null)
            {
                AllItemsAdded.Invoke(itemsAdded);
            }
        }

        public IEnumerator<TGeneric> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}