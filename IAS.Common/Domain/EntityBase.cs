using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using IAS.Common.Validator;


namespace IAS.Common.Domain
{
    public abstract class EntityBase<TId,T> : IValidatableObject
    {
        private T _entity;
        //protected String _userToken;
        public virtual TId Primary { get; set; }
        public virtual T Entity { get { return _entity; } }
        //private List<BusinessRule> _brokenRules = new List<BusinessRule>();

        private  IList<ValidationResult> _validationResults = new List<ValidationResult>();
        //private String _createBy;
        //private DateTime _createDate;
        //private String _updateBy;
        //private DateTime _updateDate;
        /// <summary>
        /// Initializes a new instance of the EntityBaseBase class.
        /// </summary>
        public EntityBase()
        {
            Init();
        }
        public EntityBase(TId primary, T entity)
        {
            SetEntity(primary, entity);
            Init();
        }
        public void SetEntity(TId primary, T entity){
            Primary = primary;
            _entity = entity;
        }
        private void Init() 
        {
            //try
            //{
            //    AspFormsAuthentication formAuth = new AspFormsAuthentication();
            //    _userToken = formAuth.GetAuthenticationToken();
            //}
            //catch (Exception)
            //{

            //}
        }

        //public String CreateBy { get { return _createBy; } set { _createBy = value; } }
        //public DateTime CreateDate { get { return _createDate; } set { _createDate = value; } }
        //public String UpdateBy { get { return _updateBy; } set { _updateBy = value; } }
        //public DateTime UpdateDate { get { return _updateDate; } set { _updateDate = value; } }

        //public void signatureNew() { signatureNew(_userToken, DateTime.Now); }
        //public void signatureNew(String signature, DateTime signDate )   
        //{

        //    CreateBy = signature;
        //    CreateDate = signDate;

        //    UpdateBy = signature;
        //    UpdateDate = signDate;
        //}

        //public void signatureUpdate() { signatureUpdate(_userToken, DateTime.Now); }
        //public void signatureUpdate(String signature, DateTime signDate) {
        //    UpdateBy = signature;
        //    UpdateDate = signDate;
        //}

        #region Validation
        public virtual  IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            _validationResults.Clear();
            Validate();
            return _validationResults;
        }
        protected abstract void Validate();

        public virtual IEnumerable<String> GetBrokenRules()
        {
            return  EntityValidatorFactory.CreateValidator().GetInvalidMessages(this);
        }
        public virtual Boolean IsValid() {
            return EntityValidatorFactory.CreateValidator().IsValid(this);
        }

        protected virtual void AddBrokenRule(ValidationResult businessRule)
        {
            _validationResults.Add(businessRule);
            //_brokenRules.Add(businessRule);
        }
        #endregion

        #region Comparision
        public override bool Equals(object entity)
        {
            return  entity != null
                && entity is EntityBase<TId, T>
                && this == (EntityBase<TId, T>)entity;
        }

        public override int GetHashCode()
        {
            return Primary.GetHashCode();
        }

        public static bool operator ==(EntityBase<TId, T> entity1, EntityBase<TId, T> entity2)
        {
            if ((Object)entity1 == null && (Object)entity2 == null)
            {
                return true;
            }
            if ((Object)entity1 == null || (Object)entity2 == null)
            {
                return false;
            }
            if (entity1.Primary.ToString() == entity2.Primary.ToString())
            {
                return true;
            }
            return false;
        }

        public static bool operator !=(EntityBase<TId, T> entity1,
            EntityBase<TId, T> entity2)
        {
            return (!(entity1 == entity2));
        }

        #endregion



    }
}
