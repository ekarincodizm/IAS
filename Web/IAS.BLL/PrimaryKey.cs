using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.BLL
{
    public class PrimaryKey<TId1>
    {
        public TId1 Id1 { get; set; }

        public override bool Equals(object entity)
        {
            return entity != null
                && entity is PrimaryKey<TId1>
                && this == (PrimaryKey<TId1>)entity;
        }

        public override int GetHashCode()
        {
            return Id1.GetHashCode();
        }

        public static bool operator ==(PrimaryKey<TId1> entity1, PrimaryKey<TId1> entity2)
        {
            if ((Object)entity1 == null && (Object)entity2 == null)
            {
                return true;
            }
            if ((Object)entity1 == null || (Object)entity2 == null)
            {
                return false;
            }
            if (entity1.Id1.ToString() == entity2.Id1.ToString())
            {
                return true;
            }
            return false;
        }

        public static bool operator !=(PrimaryKey<TId1> entity1,
            PrimaryKey<TId1> entity2)
        {
            return (!(entity1 == entity2));
        }

        public override string ToString()
        {
            var keies = new StringBuilder("Primary Key : ");
            keies.AppendLine("Id1:" + Id1.ToString());
            return keies.ToString();
        }
    }
    public class PrimaryKey<TId1, TId2>
    {
        public  TId1 Id1 { get; set; }
        public  TId2 Id2 { get; set; }

        public override bool Equals(object entity)
        {
            return entity != null
                && entity is PrimaryKey<TId1, TId2>
                && this == (PrimaryKey<TId1, TId2>)entity;
        }

        public override int GetHashCode()
        {
            var hashCode = 0;
            hashCode = 19 * hashCode + Id1.GetHashCode();
            hashCode = 19 * hashCode + Id2.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(PrimaryKey<TId1, TId2> entity1,
            PrimaryKey<TId1, TId2> entity2)
        {
            if ((Object)entity1 == null && (Object)entity2 == null)
            {
                return true;
            }
            if ((Object)entity1 == null || (Object)entity2 == null)
            {
                return false;
            }
            if (entity1.Id1.ToString() == entity2.Id1.ToString()
                && entity1.Id2.ToString() == entity2.Id2.ToString())
            {
                return true;
            }
            return false;
        }

        public static bool operator !=(PrimaryKey<TId1, TId2> entity1,
            PrimaryKey<TId1, TId2> entity2)
        {
            return (!(entity1 == entity2));
        }

        public override string ToString()
        {
            var keies = new StringBuilder("Primary Key : ");
            keies.AppendLine("Id1:" + Id1.ToString());
            keies.AppendLine("Id2:" + Id2.ToString());
            return keies.ToString();
        }
    }
    public class PrimaryKey<TId1, TId2, TId3>
    {
        public TId1 Id1 { get; set; }
        public TId2 Id2 { get; set; }
        public TId3 Id3 { get; set; }

        public override bool Equals(object entity)
        {
            return entity != null
                && entity is PrimaryKey<TId1, TId2, TId3>
                && this == (PrimaryKey<TId1, TId2, TId3>)entity;
        }

        public override int GetHashCode()
        {
            var hashCode = 0;
            hashCode = 19 * hashCode + Id1.GetHashCode();
            hashCode = 19 * hashCode + Id2.GetHashCode();
            hashCode = 19 * hashCode + Id3.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(PrimaryKey<TId1, TId2, TId3> entity1,
            PrimaryKey<TId1, TId2, TId3> entity2)
        {
            if ((Object)entity1 == null && (Object)entity2 == null)
            {
                return true;
            }
            if ((Object)entity1 == null || (Object)entity2 == null)
            {
                return false;
            }
            if (entity1.Id1.ToString() == entity2.Id1.ToString()
                && entity1.Id2.ToString() == entity2.Id2.ToString()
                && entity1.Id3.ToString() == entity2.Id3.ToString())
            {
                return true;
            }
            return false;
        }

        public static bool operator !=(PrimaryKey<TId1, TId2, TId3> entity1,
            PrimaryKey<TId1, TId2, TId3> entity2)
        {
            return (!(entity1 == entity2));
        }

        public override string ToString()
        {
            var keies = new StringBuilder("Primary Key : ");
            keies.AppendLine("Id1:" + Id1.ToString());
            keies.AppendLine("Id2:" + Id2.ToString());
            keies.AppendLine("Id3:" + Id3.ToString());
            return keies.ToString();
        }
    }
    public class PrimaryKey<TId1, TId2, TId3, TId4>
    {
        public TId1 Id1 { get; set; }
        public TId2 Id2 { get; set; }
        public TId3 Id3 { get; set; }
        public TId4 Id4 { get; set; }

        public override bool Equals(object entity)
        {
            return entity != null
                && entity is PrimaryKey<TId1, TId2, TId3, TId4>
                && this == (PrimaryKey<TId1, TId2, TId3, TId4>)entity;
        }

        public override int GetHashCode()
        {
            var hashCode = 0;
            hashCode = 19 * hashCode + Id1.GetHashCode();
            hashCode = 19 * hashCode + Id2.GetHashCode();
            hashCode = 19 * hashCode + Id3.GetHashCode();
            hashCode = 19 * hashCode + Id4.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(PrimaryKey<TId1, TId2, TId3, TId4> entity1,
            PrimaryKey<TId1, TId2, TId3, TId4> entity2)
        {
            if ((Object)entity1 == null && (Object)entity2 == null)
            {
                return true;
            }
            if ((Object)entity1 == null || (Object)entity2 == null)
            {
                return false;
            }
            if (entity1.Id1.ToString() == entity2.Id1.ToString()
                && entity1.Id2.ToString() == entity2.Id2.ToString()
                && entity1.Id3.ToString() == entity2.Id3.ToString()
                && entity1.Id4.ToString() == entity2.Id4.ToString())
            {
                return true;
            }
            return false;
        }

        public static bool operator !=(PrimaryKey<TId1, TId2, TId3, TId4> entity1,
            PrimaryKey<TId1, TId2, TId3, TId4> entity2)
        {
            return (!(entity1 == entity2));
        }

        public override string ToString()
        {
            var keies = new StringBuilder("Primary Key : ");
            keies.AppendLine("Id1:" + Id1.ToString());
            keies.AppendLine("Id2:" + Id2.ToString());
            keies.AppendLine("Id3:" + Id3.ToString());
            keies.AppendLine("Id4:" + Id4.ToString());
            return keies.ToString();
        }
    }
    public class PrimaryKey<TId1, TId2, TId3, TId4, TId5>
    {
        public TId1 Id1 { get; set; }
        public TId2 Id2 { get; set; }
        public TId3 Id3 { get; set; }
        public TId4 Id4 { get; set; }
        public TId5 Id5 { get; set; }

        public override bool Equals(object entity)
        {
            return entity != null
                && entity is PrimaryKey<TId1, TId2, TId3, TId4, TId5>
                && this == (PrimaryKey<TId1, TId2, TId3, TId4, TId5>)entity;
        }

        public override int GetHashCode()
        {
            var hashCode = 0;
            hashCode = 19 * hashCode + Id1.GetHashCode();
            hashCode = 19 * hashCode + Id2.GetHashCode();
            hashCode = 19 * hashCode + Id3.GetHashCode();
            hashCode = 19 * hashCode + Id4.GetHashCode();
            hashCode = 19 * hashCode + Id5.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(PrimaryKey<TId1, TId2, TId3, TId4, TId5> entity1,
            PrimaryKey<TId1, TId2, TId3, TId4, TId5> entity2)
        {
            if ((Object)entity1 == null && (Object)entity2 == null)
            {
                return true;
            }
            if ((Object)entity1 == null || (Object)entity2 == null)
            {
                return false;
            }
            if (entity1.Id1.ToString() == entity2.Id1.ToString()
                && entity1.Id2.ToString() == entity2.Id2.ToString()
                && entity1.Id3.ToString() == entity2.Id3.ToString()
                && entity1.Id4.ToString() == entity2.Id4.ToString()
                && entity1.Id5.ToString() == entity2.Id5.ToString())
            {
                return true;
            }
            return false;
        }

        public static bool operator !=(PrimaryKey<TId1, TId2, TId3, TId4, TId5> entity1,
            PrimaryKey<TId1, TId2, TId3, TId4, TId5> entity2)
        {
            return (!(entity1 == entity2));
        }

        public override string ToString()
        {
            var keies = new StringBuilder("Primary Key : ");
            keies.AppendLine("Id1:" + Id1.ToString());
            keies.AppendLine("Id2:" + Id2.ToString());
            keies.AppendLine("Id3:" + Id3.ToString());
            keies.AppendLine("Id4:" + Id4.ToString());
            keies.AppendLine("Id5:" + Id5.ToString());
            return keies.ToString();
        }
    }
    public class PrimaryKey<TId1, TId2, TId3, TId4, TId5, TId6>
    {
        public TId1 Id1 { get; set; }
        public TId2 Id2 { get; set; }
        public TId3 Id3 { get; set; }
        public TId4 Id4 { get; set; }
        public TId5 Id5 { get; set; }
        public TId6 Id6 { get; set; }
        public override bool Equals(object entity)
        {
            return entity != null
                && entity is PrimaryKey<TId1, TId2, TId3, TId4, TId5, TId6>
                && this == (PrimaryKey<TId1, TId2, TId3, TId4, TId5, TId6>)entity;
        }

        public override int GetHashCode()
        {
            var hashCode = 0;
            hashCode = 19 * hashCode + Id1.GetHashCode();
            hashCode = 19 * hashCode + Id2.GetHashCode();
            hashCode = 19 * hashCode + Id3.GetHashCode();
            hashCode = 19 * hashCode + Id4.GetHashCode();
            hashCode = 19 * hashCode + Id5.GetHashCode();
            hashCode = 19 * hashCode + Id6.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(PrimaryKey<TId1, TId2, TId3, TId4, TId5, TId6> entity1,
            PrimaryKey<TId1, TId2, TId3, TId4, TId5, TId6> entity2)
        {
            if ((Object)entity1 == null && (Object)entity2 == null)
            {
                return true;
            }
            if ((Object)entity1 == null || (Object)entity2 == null)
            {
                return false;
            }
            if (entity1.Id1.ToString() == entity2.Id1.ToString()
                && entity1.Id2.ToString() == entity2.Id2.ToString()
                && entity1.Id3.ToString() == entity2.Id3.ToString()
                && entity1.Id4.ToString() == entity2.Id4.ToString()
                && entity1.Id5.ToString() == entity2.Id5.ToString()
                && entity1.Id6.ToString() == entity2.Id6.ToString())
            {
                return true;
            }
            return false;
        }

        public static bool operator !=(PrimaryKey<TId1, TId2, TId3, TId4, TId5, TId6> entity1,
            PrimaryKey<TId1, TId2, TId3, TId4, TId5, TId6> entity2)
        {
            return (!(entity1 == entity2));
        }

        public override string ToString()
        {
            var keies = new StringBuilder("Primary Key : ");
            keies.AppendLine("Id1:" + Id1.ToString());
            keies.AppendLine("Id2:" + Id2.ToString());
            keies.AppendLine("Id3:" + Id3.ToString());
            keies.AppendLine("Id4:" + Id4.ToString());
            keies.AppendLine("Id5:" + Id5.ToString());
            keies.AppendLine("Id6:" + Id6.ToString());
            return keies.ToString();
        }
    }
    public class PrimaryKey<TId1, TId2, TId3, TId4, TId5, TId6, TId7>
    {
        public TId1 Id1 { get; set; }
        public TId2 Id2 { get; set; }
        public TId3 Id3 { get; set; }
        public TId4 Id4 { get; set; }
        public TId5 Id5 { get; set; }
        public TId6 Id6 { get; set; }
        public TId7 Id7 { get; set; }

        public override bool Equals(object entity)
        {
            return entity != null
                && entity is PrimaryKey<TId1, TId2, TId3, TId4, TId5, TId6, TId7>
                && this == (PrimaryKey<TId1, TId2, TId3, TId4, TId5, TId6, TId7>)entity;
        }

        public override int GetHashCode()
        {
            var hashCode = 0;
            hashCode = 19 * hashCode + Id1.GetHashCode();
            hashCode = 19 * hashCode + Id2.GetHashCode();
            hashCode = 19 * hashCode + Id3.GetHashCode();
            hashCode = 19 * hashCode + Id4.GetHashCode();
            hashCode = 19 * hashCode + Id5.GetHashCode();
            hashCode = 19 * hashCode + Id6.GetHashCode();
            hashCode = 19 * hashCode + Id7.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(PrimaryKey<TId1, TId2, TId3, TId4, TId5, TId6, TId7> entity1,
            PrimaryKey<TId1, TId2, TId3, TId4, TId5, TId6, TId7> entity2)
        {
            if ((Object)entity1 == null && (Object)entity2 == null)
            {
                return true;
            }
            if ((Object)entity1 == null || (Object)entity2 == null)
            {
                return false;
            }
            if (entity1.Id1.ToString() == entity2.Id1.ToString()
                && entity1.Id2.ToString() == entity2.Id2.ToString()
                && entity1.Id3.ToString() == entity2.Id3.ToString()
                && entity1.Id4.ToString() == entity2.Id4.ToString()
                && entity1.Id5.ToString() == entity2.Id5.ToString()
                && entity1.Id6.ToString() == entity2.Id6.ToString()
                && entity1.Id7.ToString() == entity2.Id7.ToString())
            {
                return true;
            }
            return false;
        }

        public static bool operator !=(PrimaryKey<TId1, TId2, TId3, TId4, TId5, TId6, TId7> entity1,
            PrimaryKey<TId1, TId2, TId3, TId4, TId5, TId6, TId7> entity2)
        {
            return (!(entity1 == entity2));
        }

        public override string ToString()
        {
            var keies = new StringBuilder("Primary Key : ");
            keies.AppendLine("Id1:" + Id1.ToString());
            keies.AppendLine("Id2:" + Id2.ToString());
            keies.AppendLine("Id3:" + Id3.ToString());
            keies.AppendLine("Id4:" + Id4.ToString());
            keies.AppendLine("Id5:" + Id5.ToString());
            keies.AppendLine("Id6:" + Id6.ToString());
            keies.AppendLine("Id7:" + Id7.ToString());
            return keies.ToString();
        }
    }
    public class PrimaryKey<TId1, TId2, TId3, TId4, TId5, TId6, TId7, TId8>
    {
        public TId1 Id1 { get; set; }
        public TId2 Id2 { get; set; }
        public TId3 Id3 { get; set; }
        public TId4 Id4 { get; set; }
        public TId5 Id5 { get; set; }
        public TId6 Id6 { get; set; }
        public TId7 Id7 { get; set; }
        public TId8 Id8 { get; set; }

        public override bool Equals(object entity)
        {
            return entity != null
                && entity is PrimaryKey<TId1, TId2, TId3, TId4, TId5, TId6, TId7, TId8>
                && this == (PrimaryKey<TId1, TId2, TId3, TId4, TId5, TId6, TId7, TId8>)entity;
        }

        public override int GetHashCode()
        {
            var hashCode = 0;
            hashCode = 19 * hashCode + Id1.GetHashCode();
            hashCode = 19 * hashCode + Id2.GetHashCode();
            hashCode = 19 * hashCode + Id3.GetHashCode();
            hashCode = 19 * hashCode + Id4.GetHashCode();
            hashCode = 19 * hashCode + Id5.GetHashCode();
            hashCode = 19 * hashCode + Id6.GetHashCode();
            hashCode = 19 * hashCode + Id7.GetHashCode();
            hashCode = 19 * hashCode + Id8.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(PrimaryKey<TId1, TId2, TId3, TId4, TId5, TId6, TId7, TId8> entity1,
            PrimaryKey<TId1, TId2, TId3, TId4, TId5, TId6, TId7, TId8> entity2)
        {
            if ((Object)entity1 == null && (Object)entity2 == null)
            {
                return true;
            }
            if ((Object)entity1 == null || (Object)entity2 == null)
            {
                return false;
            }
            if (entity1.Id1.ToString() == entity2.Id1.ToString()
                && entity1.Id2.ToString() == entity2.Id2.ToString()
                && entity1.Id3.ToString() == entity2.Id3.ToString()
                && entity1.Id4.ToString() == entity2.Id4.ToString()
                && entity1.Id5.ToString() == entity2.Id5.ToString()
                && entity1.Id6.ToString() == entity2.Id6.ToString()
                && entity1.Id7.ToString() == entity2.Id7.ToString()
                && entity1.Id8.ToString() == entity2.Id8.ToString())
            {
                return true;
            }
            return false;
        }

        public static bool operator !=(PrimaryKey<TId1, TId2, TId3, TId4, TId5, TId6, TId7, TId8> entity1,
            PrimaryKey<TId1, TId2, TId3, TId4, TId5, TId6, TId7, TId8> entity2)
        {
            return (!(entity1 == entity2));
        }

        public override string ToString()
        {
            var keies = new StringBuilder("Primary Key : ");
            keies.AppendLine("Id1:" + Id1.ToString());
            keies.AppendLine("Id2:" + Id2.ToString());
            keies.AppendLine("Id3:" + Id3.ToString());
            keies.AppendLine("Id4:" + Id4.ToString());
            keies.AppendLine("Id5:" + Id5.ToString());
            keies.AppendLine("Id6:" + Id6.ToString());
            keies.AppendLine("Id7:" + Id7.ToString());
            keies.AppendLine("Id8:" + Id8.ToString());
            return keies.ToString();
        }
    }
    public class PrimaryKey<TId1, TId2, TId3, TId4, TId5, TId6, TId7, TId8, TId9>
    {
        public TId1 Id1 { get; set; }
        public TId2 Id2 { get; set; }
        public TId3 Id3 { get; set; }
        public TId4 Id4 { get; set; }
        public TId5 Id5 { get; set; }
        public TId6 Id6 { get; set; }
        public TId7 Id7 { get; set; }
        public TId8 Id8 { get; set; }
        public TId9 Id9 { get; set; }

        public override bool Equals(object entity)
        {
            return entity != null
                && entity is PrimaryKey<TId1, TId2, TId3, TId4, TId5, TId6, TId7, TId8, TId9>
                && this == (PrimaryKey<TId1, TId2, TId3, TId4, TId5, TId6, TId7, TId8, TId9>)entity;
        }

        public override int GetHashCode()
        {
            var hashCode = 0;
            hashCode = 19 * hashCode + Id1.GetHashCode();
            hashCode = 19 * hashCode + Id2.GetHashCode();
            hashCode = 19 * hashCode + Id3.GetHashCode();
            hashCode = 19 * hashCode + Id4.GetHashCode();
            hashCode = 19 * hashCode + Id5.GetHashCode();
            hashCode = 19 * hashCode + Id6.GetHashCode();
            hashCode = 19 * hashCode + Id7.GetHashCode();
            hashCode = 19 * hashCode + Id8.GetHashCode();
            hashCode = 19 * hashCode + Id9.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(PrimaryKey<TId1, TId2, TId3, TId4, TId5, TId6, TId7, TId8, TId9> entity1,
            PrimaryKey<TId1, TId2, TId3, TId4, TId5, TId6, TId7, TId8, TId9> entity2)
        {
            if ((Object)entity1 == null && (Object)entity2 == null)
            {
                return true;
            }
            if ((Object)entity1 == null || (Object)entity2 == null)
            {
                return false;
            }
            if (entity1.Id1.ToString() == entity2.Id1.ToString()
                && entity1.Id2.ToString() == entity2.Id2.ToString()
                && entity1.Id3.ToString() == entity2.Id3.ToString()
                && entity1.Id4.ToString() == entity2.Id4.ToString()
                && entity1.Id5.ToString() == entity2.Id5.ToString()
                && entity1.Id6.ToString() == entity2.Id6.ToString()
                && entity1.Id7.ToString() == entity2.Id7.ToString()
                && entity1.Id8.ToString() == entity2.Id8.ToString()
                && entity1.Id9.ToString() == entity2.Id9.ToString())
            {
                return true;
            }
            return false;
        }

        public static bool operator !=(PrimaryKey<TId1, TId2, TId3, TId4, TId5, TId6, TId7, TId8, TId9> entity1,
            PrimaryKey<TId1, TId2, TId3, TId4, TId5, TId6, TId7, TId8, TId9> entity2)
        {
            return (!(entity1 == entity2));
        }

        public override string ToString()
        {
            var keies = new StringBuilder("Primary Key : ");
            keies.AppendLine("Id1:" + Id1.ToString());
            keies.AppendLine("Id2:" + Id2.ToString());
            keies.AppendLine("Id3:" + Id3.ToString());
            keies.AppendLine("Id4:" + Id4.ToString());
            keies.AppendLine("Id5:" + Id5.ToString());
            keies.AppendLine("Id6:" + Id6.ToString());
            keies.AppendLine("Id7:" + Id7.ToString());
            keies.AppendLine("Id8:" + Id8.ToString());
            keies.AppendLine("Id9:" + Id9.ToString());
            return keies.ToString();
        }
    }
    public class PrimaryKey<TId1, TId2, TId3, TId4, TId5, TId6, TId7, TId8, TId9, TId10>
    {
        public TId1 Id1 { get; set; }
        public TId2 Id2 { get; set; }
        public TId3 Id3 { get; set; }
        public TId4 Id4 { get; set; }
        public TId5 Id5 { get; set; }
        public TId6 Id6 { get; set; }
        public TId7 Id7 { get; set; }
        public TId8 Id8 { get; set; }
        public TId9 Id9 { get; set; }
        public TId10 Id10 { get; set; }

        public override bool Equals(object entity)
        {
            return entity != null
                && entity is PrimaryKey<TId1, TId2, TId3, TId4, TId5, TId6, TId7, TId8, TId9, TId10>
                && this == (PrimaryKey<TId1, TId2, TId3, TId4, TId5, TId6, TId7, TId8, TId9, TId10>)entity;
        }

        public override int GetHashCode()
        {
            var hashCode = 0;
            hashCode = 19 * hashCode + Id1.GetHashCode();
            hashCode = 19 * hashCode + Id2.GetHashCode();
            hashCode = 19 * hashCode + Id3.GetHashCode();
            hashCode = 19 * hashCode + Id4.GetHashCode();
            hashCode = 19 * hashCode + Id5.GetHashCode();
            hashCode = 19 * hashCode + Id6.GetHashCode();
            hashCode = 19 * hashCode + Id7.GetHashCode();
            hashCode = 19 * hashCode + Id8.GetHashCode();
            hashCode = 19 * hashCode + Id9.GetHashCode();
            hashCode = 19 * hashCode + Id10.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(PrimaryKey<TId1, TId2, TId3, TId4, TId5, TId6, TId7, TId8, TId9, TId10> entity1,
            PrimaryKey<TId1, TId2, TId3, TId4, TId5, TId6, TId7, TId8, TId9, TId10> entity2)
        {
            if ((Object)entity1 == null && (Object)entity2 == null)
            {
                return true;
            }
            if ((Object)entity1 == null || (Object)entity2 == null)
            {
                return false;
            }
            if (entity1.Id1.ToString() == entity2.Id1.ToString()
                && entity1.Id2.ToString() == entity2.Id2.ToString()
                && entity1.Id3.ToString() == entity2.Id3.ToString()
                && entity1.Id4.ToString() == entity2.Id4.ToString()
                && entity1.Id5.ToString() == entity2.Id5.ToString()
                && entity1.Id6.ToString() == entity2.Id6.ToString()
                && entity1.Id7.ToString() == entity2.Id7.ToString()
                && entity1.Id8.ToString() == entity2.Id8.ToString()
                && entity1.Id9.ToString() == entity2.Id9.ToString()
                && entity1.Id9.ToString() == entity2.Id10.ToString())
            {
                return true;
            }
            return false;
        }

        public static bool operator !=(PrimaryKey<TId1, TId2, TId3, TId4, TId5, TId6, TId7, TId8, TId9, TId10> entity1,
            PrimaryKey<TId1, TId2, TId3, TId4, TId5, TId6, TId7, TId8, TId9, TId10> entity2)
        {
            return (!(entity1 == entity2));
        }

        public override string ToString()
        {
            var keies = new StringBuilder("Primary Key : ");
            keies.AppendLine("Id1:" + Id1.ToString());
            keies.AppendLine("Id2:" + Id2.ToString());
            keies.AppendLine("Id3:" + Id3.ToString());
            keies.AppendLine("Id4:" + Id4.ToString());
            keies.AppendLine("Id5:" + Id5.ToString());
            keies.AppendLine("Id6:" + Id6.ToString());
            keies.AppendLine("Id7:" + Id7.ToString());
            keies.AppendLine("Id8:" + Id8.ToString());
            keies.AppendLine("Id9:" + Id9.ToString());
            keies.AppendLine("Id10:" + Id10.ToString());
            return keies.ToString();
        }
    }
}
