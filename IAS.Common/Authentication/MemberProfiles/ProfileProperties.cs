using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.Common.Authentication.MemberProfiles
{
    [Serializable]
    public class ProfileProperties
    {
        public Guid ProfileId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }

        public Guid MemberGroupId { get; set; }

        public ProfileAddressList AddressProperties { get; set; }
        public ProfilePhoneList PhoneProperties { get; set; }
    }

    [Serializable]
    public class ProfileAddressList : IList<AddressProperty>
    {
        IList<AddressProperty> collection = new List<AddressProperty>();
        public int IndexOf(AddressProperty item)
        {
            return collection.IndexOf(item) ;
        }

        public void Insert(int index, AddressProperty item)
        {
            collection.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            collection.RemoveAt(index);
        }

        public AddressProperty this[int index]
        {
            get
            {
                return collection[index];
            }
            set
            {
                collection[index] = value;
            }
        }

        public void Add(AddressProperty item)
        {
            collection.Add(item);
        }

        public void Clear()
        {
            collection.Clear();
        }

        public bool Contains(AddressProperty item)
        {
            return collection.Contains(item);
        }

        public void CopyTo(AddressProperty[] array, int arrayIndex)
        {
            collection.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return collection.Count; }
        }

        public bool IsReadOnly
        {
            get { return collection.IsReadOnly; }
        }

        public bool Remove(AddressProperty item)
        {
            return collection.Remove(item);
        }

        public IEnumerator<AddressProperty> GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (AddressProperty element in collection) 
            {
                yield return element;
            }
        }
    }

    [Serializable]
    public class ProfilePhoneList : IList<PhoneProperty>
    {
        IList<PhoneProperty> collection = new List<PhoneProperty>();
        public int IndexOf(PhoneProperty item)
        {
            return collection.IndexOf(item);
        }

        public void Insert(int index, PhoneProperty item)
        {
            collection.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            collection.RemoveAt(index);
        }

        public PhoneProperty this[int index]
        {
            get
            {
                return collection[index];
            }
            set
            {
                collection[index] = value;
            }
        }

        public void Add(PhoneProperty item)
        {
            collection.Add(item);
        }

        public void Clear()
        {
            collection.Clear();
        }

        public bool Contains(PhoneProperty item)
        {
            return collection.Contains(item);
        }

        public void CopyTo(PhoneProperty[] array, int arrayIndex)
        {
            collection.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get {  return collection.Count; }
        }

        public bool IsReadOnly
        {
            get { return collection.IsReadOnly; }
        }

        public bool Remove(PhoneProperty item)
        {
            return collection.Remove(item);
        }

        public IEnumerator<PhoneProperty> GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (PhoneProperty element in collection)
                yield return element;
        }
    }

    [Serializable]
    public class AddressProperty 
    {
        public AddressTypeProperty AddressType { get; set; }
        public Guid AddressId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Country { get; set; }
        public string State { get; set; }                       
        public string City { get; set; }
        public string ZipCode { get; set; }


    }

    [Serializable]
    public class AddressTypeProperty
    {
        public int AddressTypeId { get; set; }
        public string Name { get; set; }
    }

    [Serializable]
    public class PhoneProperty
    {
        public PhoneTypeProperty PhoneType { get; set; }
        public Guid PhoneId { get; set; }
        public string Number { get; set; }  
    }

     [Serializable]
    public class PhoneTypeProperty
    {
        public int PhoneTypeId { get; set; }  
        public string Name { get; set; }  
    }
}
