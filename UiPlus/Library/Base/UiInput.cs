using Grasshopper.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiPlus.Elements
{
    public class UiInput
    {
        #region Members
        public enum InputTypes { Param_GenericObject, Param_Integer, Param_Number, Param_String, Param_Boolean, Param_Colour }
        protected InputTypes inputType = InputTypes.Param_GenericObject;
        protected string name = string.Empty;
        protected string nickName = string.Empty;
        protected string description = string.Empty;
        protected GH_ParamAccess access = GH_ParamAccess.item;

        #endregion

        #region Constructors

        public UiInput(UiInput uiInput)
        {
            this.inputType = uiInput.inputType;

            this.name = uiInput.name;
            this.nickName = uiInput.nickName;
            this.description = uiInput.description;

            this.access = uiInput.access;
        }

        public UiInput(InputTypes inputType, string name, string nickName, string description, GH_ParamAccess access)
        {
            this.inputType = inputType;

            this.name = name;
            this.nickName = nickName;
            this.description = description;

            this.access = access;

        }

        #endregion

        #region Properties

        public virtual InputTypes InputType
        {
            get { return inputType; }
        }

        public virtual string Name
        {
            get { return name; }
        }

        public virtual string NickName
        {
            get { return nickName; }
        }

        public virtual string Description
        {
            get { return description; }
        }

        public virtual GH_ParamAccess Access
        {
            get { return access; }
        }

        #endregion

    }
}
