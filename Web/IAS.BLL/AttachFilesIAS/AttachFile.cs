using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.BLL.AttachFilesIAS
{
    public class AttachFile : EntityBase<PrimaryKey<String,String>>, IAttachFile
    {
        /// <summary>
        /// Initializes a new instance of the AttachFile class.
        /// </summary>
        public AttachFile(  )
        {
            Init();
            base.Id = new PrimaryKey<string, string>();

        }
      
        public void Init()
        {
           
        }


        public String ID {
            get { return base.Id.Id1; }
            set { base.Id.Id1 = value; } 
        } //	VARCHAR2	(	15	)


        public String REGISTRATION_ID { get; set; } //	VARCHAR2	(	15	)
        public String ATTACH_FILE_TYPE 
        {
            get { return base.Id.Id2; }
            set { base.Id.Id2 = value; } 
        } //	VARCHAR2	(	4	)

        public String ATTACH_FILE_PATH { get; set; } //	VARCHAR2	(	100	)
        
        public String REMARK { get; set; } //	VARCHAR2	(	100	)
        public String CREATED_BY { get; set; } //	VARCHAR2	(	20	)
        public DateTime CREATED_DATE { get; set; } //	DATE	(	7	)
        public String UPDATED_BY { get; set; } //	VARCHAR2	(	20	)
        public DateTime UPDATED_DATE { get; set; } //	DATE	(	7	)
        public String FILE_STATUS { get; set; } //	VARCHAR2	(	1	)

        public String LICENSE_NO { get; set; } //	VARCHAR2	(	15	)
        public String RENEW_TIME { get; set; } //	VARCHAR2	(	2	)
        public String GROUP_LICENSE_ID { get; set; } //	VARCHAR2	(	15	)

        public String REQUEST_STATUS { get; set; }
        public String ID_CARD_NO { get; set; }

        public String ATTACH_FILE_NAME { 
            get {
                String[] arrName = this.ATTACH_FILE_PATH.Split('\\');
                return (arrName.Length <= 0)? "": arrName[arrName.Length-1]; 
            }
        }

        public String ATTACH_CONTAINER { 
            get{
                String[] arrContainer = this.ATTACH_FILE_PATH.Split('\\');
                String containerName = "";
                if(arrContainer.Length >= 2){
                    for(int i = 0;i < (arrContainer.Length-1); i++)
	                {
                        containerName +=   (i==(arrContainer.Length-1))?arrContainer[i]: arrContainer[i]+@"\";
	                }
                }
                return containerName;
            }
        }
        public String EXTENSION
        {
            get
            {
                //String[] result  =ATTACH_FILE_NAME.Split('.');
                //String extension = (result.Length > 1)?"."+result[1]: "";
                String extension = ATTACH_FILE_NAME.Substring(ATTACH_FILE_NAME.LastIndexOf('.'));
                return extension;
            }
        }



        protected override void Validate()
        {
            if (String.IsNullOrEmpty(ID)) 
                base.AddBrokenRule(AttachFileBusinessRule.ID_Required);

            if (String.IsNullOrEmpty(REGISTRATION_ID)) 
                base.AddBrokenRule(AttachFileBusinessRule.REGISTRATION_ID_Required);

            if (String.IsNullOrEmpty(ATTACH_FILE_TYPE)) 
                base.AddBrokenRule(AttachFileBusinessRule.ATTACH_FILE_TYPE_Required);

            if (String.IsNullOrEmpty(ATTACH_FILE_PATH)) 
                base.AddBrokenRule(AttachFileBusinessRule.ATTACH_FILE_PATH_Required);

            //if (String.IsNullOrEmpty(REMARK)) 
            //    base.AddBrokenRule(AttachFileBusinessRule.REMARK_Required);

            if (String.IsNullOrEmpty(CREATED_BY)) 
                base.AddBrokenRule(AttachFileBusinessRule.CREATED_BY_Required);

            if (CREATED_DATE == DateTime.MinValue) 
                base.AddBrokenRule(AttachFileBusinessRule.CREATED_DATE_Required);

            if (String.IsNullOrEmpty(UPDATED_BY)) 
                base.AddBrokenRule(AttachFileBusinessRule.UPDATED_BY_Required);

            if (UPDATED_DATE == DateTime.MinValue) 
                base.AddBrokenRule(AttachFileBusinessRule.UPDATED_DATE_Required);

            if (String.IsNullOrEmpty(FILE_STATUS)) 
                base.AddBrokenRule(AttachFileBusinessRule.FILE_STATUS_Required);
        }
    }
}
