using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArmoryLib.Character
{
    [AttributeUsage(AttributeTargets.Property)]
    sealed class RequiredDetailAttribute : Attribute
    {
        private readonly CharacterDetail _requiredDetail;

        public RequiredDetailAttribute(CharacterDetail requiredDetail)
        {
            _requiredDetail = requiredDetail;
        }
    }
}
