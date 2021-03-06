﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DocDb.Mongo.Abstracts;
using BindingFlags = System.Reflection.BindingFlags;

namespace DocDb.Mongo.Models
{
    internal class TrackingList<T> : ICollection<T>, IList<T>, ITrackingList<T>
    {
        public static TrackingList<T> CreateNewTrackingList(IEnumerable<T> entities)
        {
            return new TrackingList<T>
            {
                AddedList = new List<T>(entities)
            };
        }

        public static TrackingList<T> CreateExistingTrackingList(IEnumerable<T> entities)
        {
            return new TrackingList<T>
            {
                CurrentList = new List<T>(entities)
            };
        }

        public TrackingList()
        {
            this.RemovedList = new List<T>();
            this.CurrentList = new List<T>();
            this.AddedList = new List<T>();
        }

        //public TrackingList(IEnumerable<T> items)
        //{
        //    items = items.ToArray();

        //    this.CurrentList = new List<T>(items);
        //    this.AddedList = new List<T>(items);
        //    this.RemovedList = new List<T>();
        //}

        public List<T> RemovedList { get; private set; }

        public List<T> AddedList { get; private set; }

        public List<T> CurrentList { get; private set; }

        public int Count { get { return this.CurrentList.Count; } }

        public bool IsReadOnly { get; } = false;

        public T this[int index]
        {
            get => this.CurrentList[index];
            set
            {
                var item = this.CurrentList[index];
                if (item != null)
                {
                    this.RemovedList.Add(item);
                }
                this.CurrentList[index] = value;
                this.AddedList[index] = value;
            }
        }

        public void Add(T item)
        {
            CurrentList.Add(item);
            AddedList.Add(item);
        }

        public void Clear()
        {
            RemovedList.AddRange(CurrentList);
            AddedList.Clear();
            CurrentList.Clear();
        }

        public bool Contains(T item)
        {
            return this.CurrentList.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.CurrentList.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            this.CurrentList.Remove(item);
            this.RemovedList.Add(item);
            return this.AddedList.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.CurrentList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.CurrentList.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return this.CurrentList.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            this.AddedList.Insert(index, item);
            this.CurrentList.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            var item = this.CurrentList[index];
            this.RemovedList.Add(item);
            this.AddedList.RemoveAt(index);
            this.CurrentList.RemoveAt(index);
        }
    }
}
