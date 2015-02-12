using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Objects;

namespace IAS.DataServices.Test.Mocking
{
    public partial class MockObjectSet<T> :   IObjectSet<T>, IQueryable<T>, IEnumerable<T>, IQueryable, IEnumerable where T : class
    {
        private readonly IList<T> collection = new List<T>();
        private String _className;
        public MockObjectSet(String className)
        {
            _className = className;
        }
        #region IObjectSet<T> Members
 
        public void AddObject(T entity)
        {
            collection.Add(entity);
        }
 
        public void Attach(T entity)
        {
            collection.Add(entity);
        }
 
        public void DeleteObject(T entity)
        {
            collection.Remove(entity);
        }
 
        public void Detach(T entity)
        {
            collection.Remove(entity);
        }
 
        #endregion
 
        #region IEnumerable<T> Members
 
        public IEnumerator<T> GetEnumerator()
        {
            return collection.GetEnumerator();
        }
 
        #endregion
 
        #region IEnumerable Members
 
        IEnumerator IEnumerable.GetEnumerator()
        {
            return collection.GetEnumerator();
        }
 
        #endregion
 
        #region IQueryable<T> Members
 
        public Type ElementType
        {
            get { return typeof(T); }
        }
 
        public System.Linq.Expressions.Expression Expression
        {
            get { return collection.AsQueryable<T>().Expression; }
        }
 
        public IQueryProvider Provider
        {
            get { return collection.AsQueryable<T>().Provider; }
        }
        
        #endregion
    }
}