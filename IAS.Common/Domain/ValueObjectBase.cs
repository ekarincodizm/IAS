using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.ComponentModel.DataAnnotations;
using IAS.Common.Validator;

namespace IAS.Common.Domain
{
    public abstract class ValueObjectBase : IValidatableObject
    {
        private List<BusinessRule> _brokenRules = new List<BusinessRule>();
        private IList<ValidationResult> _validationResults = new List<ValidationResult>();
       
        public ValueObjectBase()
        {
        }

        protected abstract void Validate();

        public void ThrowExceptionIfInvalid()
        {
            _brokenRules.Clear();
            Validate();
            if (_validationResults.Count() > 0)
            {
                StringBuilder issues = new StringBuilder();
                foreach (String businessRule in EntityValidatorFactory.CreateValidator().GetInvalidMessages(this))
                    issues.AppendLine(businessRule);

                throw new ValueObjectIsInvalidException(issues.ToString());
            }
        }

        protected void AddBrokenRule(ValidationResult businessRule)
        {
            _validationResults.Add(businessRule);
            //_brokenRules.Add(businessRule);
        }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            _validationResults.Clear();
            Validate();
            return _validationResults;
        }
    }
}