using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using IAS.DAL.Interfaces;
using IAS.DAL;

namespace IAS.DataServices.Test.Mocking
{
    public class MockIASPersonEntities : IIASPersonEntities
    {

        #region IObjectSet Properties

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_AGENT_LICENSE_JURISTIC_T> AG_AGENT_LICENSE_JURISTIC_T
        {
            get
            {
                if ((_AG_AGENT_LICENSE_JURISTIC_T == null))
                {
                    _AG_AGENT_LICENSE_JURISTIC_T = new MockObjectSet<AG_AGENT_LICENSE_JURISTIC_T>("AG_AGENT_LICENSE_JURISTIC_T");
                }
                return _AG_AGENT_LICENSE_JURISTIC_T;
            }
        }
        private IObjectSet<AG_AGENT_LICENSE_JURISTIC_T> _AG_AGENT_LICENSE_JURISTIC_T;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_AGENT_LICENSE_PERSON_T> AG_AGENT_LICENSE_PERSON_T
        {
            get
            {
                if ((_AG_AGENT_LICENSE_PERSON_T == null))
                {
                    _AG_AGENT_LICENSE_PERSON_T = new MockObjectSet<AG_AGENT_LICENSE_PERSON_T>("AG_AGENT_LICENSE_PERSON_T");
                }
                return _AG_AGENT_LICENSE_PERSON_T;
            }
        }
        private IObjectSet<AG_AGENT_LICENSE_PERSON_T> _AG_AGENT_LICENSE_PERSON_T;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_AGENT_LICENSE_T> AG_AGENT_LICENSE_T
        {
            get
            {
                if ((_AG_AGENT_LICENSE_T == null))
                {
                    _AG_AGENT_LICENSE_T = new MockObjectSet<AG_AGENT_LICENSE_T>("AG_AGENT_LICENSE_T");
                }
                return _AG_AGENT_LICENSE_T;
            }
        }
        private IObjectSet<AG_AGENT_LICENSE_T> _AG_AGENT_LICENSE_T;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_APP_RUNNING_NO_T> AG_APP_RUNNING_NO_T
        {
            get
            {
                if ((_AG_APP_RUNNING_NO_T == null))
                {
                    _AG_APP_RUNNING_NO_T = new MockObjectSet<AG_APP_RUNNING_NO_T>("AG_APP_RUNNING_NO_T");
                }
                return _AG_APP_RUNNING_NO_T;
            }
        }
        private IObjectSet<AG_APP_RUNNING_NO_T> _AG_APP_RUNNING_NO_T;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_APPLICANT_SCORE_T> AG_APPLICANT_SCORE_T
        {
            get
            {
                if ((_AG_APPLICANT_SCORE_T == null))
                {
                    _AG_APPLICANT_SCORE_T = new MockObjectSet<AG_APPLICANT_SCORE_T>("AG_APPLICANT_SCORE_T");
                }
                return _AG_APPLICANT_SCORE_T;
            }
        }
        private IObjectSet<AG_APPLICANT_SCORE_T> _AG_APPLICANT_SCORE_T;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_APPLICANT_T> AG_APPLICANT_T
        {
            get
            {
                if ((_AG_APPLICANT_T == null))
                {
                    _AG_APPLICANT_T = new MockObjectSet<AG_APPLICANT_T>("AG_APPLICANT_T");
                }
                return _AG_APPLICANT_T;
            }
        }
        private IObjectSet<AG_APPLICANT_T> _AG_APPLICANT_T;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_EDUCATION_R> AG_EDUCATION_R
        {
            get
            {
                if ((_AG_EDUCATION_R == null))
                {
                    _AG_EDUCATION_R = new MockObjectSet<AG_EDUCATION_R>("AG_EDUCATION_R");
                }
                return _AG_EDUCATION_R;
            }
        }
        private IObjectSet<AG_EDUCATION_R> _AG_EDUCATION_R;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_EXAM_LICENSE_R> AG_EXAM_LICENSE_R
        {
            get
            {
                if ((_AG_EXAM_LICENSE_R == null))
                {
                    _AG_EXAM_LICENSE_R = new MockObjectSet<AG_EXAM_LICENSE_R>("AG_EXAM_LICENSE_R");
                }
                return _AG_EXAM_LICENSE_R;
            }
        }
        private IObjectSet<AG_EXAM_LICENSE_R> _AG_EXAM_LICENSE_R;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_EXAM_PLACE_GROUP_R> AG_EXAM_PLACE_GROUP_R
        {
            get
            {
                if ((_AG_EXAM_PLACE_GROUP_R == null))
                {
                    _AG_EXAM_PLACE_GROUP_R = new MockObjectSet<AG_EXAM_PLACE_GROUP_R>("AG_EXAM_PLACE_GROUP_R");
                }
                return _AG_EXAM_PLACE_GROUP_R;
            }
        }
        private IObjectSet<AG_EXAM_PLACE_GROUP_R> _AG_EXAM_PLACE_GROUP_R;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_EXAM_PLACE_R> AG_EXAM_PLACE_R
        {
            get
            {
                if ((_AG_EXAM_PLACE_R == null))
                {
                    _AG_EXAM_PLACE_R = new MockObjectSet<AG_EXAM_PLACE_R>("AG_EXAM_PLACE_R");
                }
                return _AG_EXAM_PLACE_R;
            }
        }
        private IObjectSet<AG_EXAM_PLACE_R> _AG_EXAM_PLACE_R;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_EXAM_TIME_R> AG_EXAM_TIME_R
        {
            get
            {
                if ((_AG_EXAM_TIME_R == null))
                {
                    _AG_EXAM_TIME_R = new MockObjectSet<AG_EXAM_TIME_R>("AG_EXAM_TIME_R");
                }
                return _AG_EXAM_TIME_R;
            }
        }
        private IObjectSet<AG_EXAM_TIME_R> _AG_EXAM_TIME_R;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_APPLICANT_HEADER_T> AG_IAS_APPLICANT_HEADER_T
        {
            get
            {
                if ((_AG_IAS_APPLICANT_HEADER_T == null))
                {
                    _AG_IAS_APPLICANT_HEADER_T = new MockObjectSet<AG_IAS_APPLICANT_HEADER_T>("AG_IAS_APPLICANT_HEADER_T");
                }
                return _AG_IAS_APPLICANT_HEADER_T;
            }
        }
        private IObjectSet<AG_IAS_APPLICANT_HEADER_T> _AG_IAS_APPLICANT_HEADER_T;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_APPLICANT_HEADER_TEMP> AG_IAS_APPLICANT_HEADER_TEMP
        {
            get
            {
                if ((_AG_IAS_APPLICANT_HEADER_TEMP == null))
                {
                    _AG_IAS_APPLICANT_HEADER_TEMP = new MockObjectSet<AG_IAS_APPLICANT_HEADER_TEMP>("AG_IAS_APPLICANT_HEADER_TEMP");
                }
                return _AG_IAS_APPLICANT_HEADER_TEMP;
            }
        }
        private IObjectSet<AG_IAS_APPLICANT_HEADER_TEMP> _AG_IAS_APPLICANT_HEADER_TEMP;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_APPROVE_CONFIG> AG_IAS_APPROVE_CONFIG
        {
            get
            {
                if ((_AG_IAS_APPROVE_CONFIG == null))
                {
                    _AG_IAS_APPROVE_CONFIG = new MockObjectSet<AG_IAS_APPROVE_CONFIG>("AG_IAS_APPROVE_CONFIG");
                }
                return _AG_IAS_APPROVE_CONFIG;
            }
        }
        private IObjectSet<AG_IAS_APPROVE_CONFIG> _AG_IAS_APPROVE_CONFIG;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_APPROVE_DOC_TYPE> AG_IAS_APPROVE_DOC_TYPE
        {
            get
            {
                if ((_AG_IAS_APPROVE_DOC_TYPE == null))
                {
                    _AG_IAS_APPROVE_DOC_TYPE = new MockObjectSet<AG_IAS_APPROVE_DOC_TYPE>("AG_IAS_APPROVE_DOC_TYPE");
                }
                return _AG_IAS_APPROVE_DOC_TYPE;
            }
        }
        private IObjectSet<AG_IAS_APPROVE_DOC_TYPE> _AG_IAS_APPROVE_DOC_TYPE;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_ATTACH_FILE> AG_IAS_ATTACH_FILE
        {
            get
            {
                if ((_AG_IAS_ATTACH_FILE == null))
                {
                    _AG_IAS_ATTACH_FILE = new MockObjectSet<AG_IAS_ATTACH_FILE>("AG_IAS_ATTACH_FILE");
                }
                return _AG_IAS_ATTACH_FILE;
            }
        }
        private IObjectSet<AG_IAS_ATTACH_FILE> _AG_IAS_ATTACH_FILE;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_FUNCTION_R> AG_IAS_FUNCTION_R
        {
            get
            {
                if ((_AG_IAS_FUNCTION_R == null))
                {
                    _AG_IAS_FUNCTION_R = new MockObjectSet<AG_IAS_FUNCTION_R>("AG_IAS_FUNCTION_R");
                }
                return _AG_IAS_FUNCTION_R;
            }
        }
        private IObjectSet<AG_IAS_FUNCTION_R> _AG_IAS_FUNCTION_R;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_MEMBER_TYPE> AG_IAS_MEMBER_TYPE
        {
            get
            {
                if ((_AG_IAS_MEMBER_TYPE == null))
                {
                    _AG_IAS_MEMBER_TYPE = new MockObjectSet<AG_IAS_MEMBER_TYPE>("AG_IAS_MEMBER_TYPE");
                }
                return _AG_IAS_MEMBER_TYPE;
            }
        }
        private IObjectSet<AG_IAS_MEMBER_TYPE> _AG_IAS_MEMBER_TYPE;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_NATIONALITY> AG_IAS_NATIONALITY
        {
            get
            {
                if ((_AG_IAS_NATIONALITY == null))
                {
                    _AG_IAS_NATIONALITY = new MockObjectSet<AG_IAS_NATIONALITY>("AG_IAS_NATIONALITY");
                }
                return _AG_IAS_NATIONALITY;
            }
        }
        private IObjectSet<AG_IAS_NATIONALITY> _AG_IAS_NATIONALITY;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_OIC_TYPE> AG_IAS_OIC_TYPE
        {
            get
            {
                if ((_AG_IAS_OIC_TYPE == null))
                {
                    _AG_IAS_OIC_TYPE = new MockObjectSet<AG_IAS_OIC_TYPE>("AG_IAS_OIC_TYPE");
                }
                return _AG_IAS_OIC_TYPE;
            }
        }
        private IObjectSet<AG_IAS_OIC_TYPE> _AG_IAS_OIC_TYPE;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_RESET_HISTORY> AG_IAS_RESET_HISTORY
        {
            get
            {
                if ((_AG_IAS_RESET_HISTORY == null))
                {
                    _AG_IAS_RESET_HISTORY = new MockObjectSet<AG_IAS_RESET_HISTORY>("AG_IAS_RESET_HISTORY");
                }
                return _AG_IAS_RESET_HISTORY;
            }
        }
        private IObjectSet<AG_IAS_RESET_HISTORY> _AG_IAS_RESET_HISTORY;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_TEMP_ATTACH_FILE> AG_IAS_TEMP_ATTACH_FILE
        {
            get
            {
                if ((_AG_IAS_TEMP_ATTACH_FILE == null))
                {
                    _AG_IAS_TEMP_ATTACH_FILE = new MockObjectSet<AG_IAS_TEMP_ATTACH_FILE>("AG_IAS_TEMP_ATTACH_FILE");
                }
                return _AG_IAS_TEMP_ATTACH_FILE;
            }
        }
        private IObjectSet<AG_IAS_TEMP_ATTACH_FILE> _AG_IAS_TEMP_ATTACH_FILE;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_LICENSE_RENEW_LOAD> AG_LICENSE_RENEW_LOAD
        {
            get
            {
                if ((_AG_LICENSE_RENEW_LOAD == null))
                {
                    _AG_LICENSE_RENEW_LOAD = new MockObjectSet<AG_LICENSE_RENEW_LOAD>("AG_LICENSE_RENEW_LOAD");
                }
                return _AG_LICENSE_RENEW_LOAD;
            }
        }
        private IObjectSet<AG_LICENSE_RENEW_LOAD> _AG_LICENSE_RENEW_LOAD;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_LICENSE_RUNNING_NO_T> AG_LICENSE_RUNNING_NO_T
        {
            get
            {
                if ((_AG_LICENSE_RUNNING_NO_T == null))
                {
                    _AG_LICENSE_RUNNING_NO_T = new MockObjectSet<AG_LICENSE_RUNNING_NO_T>("AG_LICENSE_RUNNING_NO_T");
                }
                return _AG_LICENSE_RUNNING_NO_T;
            }
        }
        private IObjectSet<AG_LICENSE_RUNNING_NO_T> _AG_LICENSE_RUNNING_NO_T;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_LICENSE_T> AG_LICENSE_T
        {
            get
            {
                if ((_AG_LICENSE_T == null))
                {
                    _AG_LICENSE_T = new MockObjectSet<AG_LICENSE_T>("AG_LICENSE_T");
                }
                return _AG_LICENSE_T;
            }
        }
        private IObjectSet<AG_LICENSE_T> _AG_LICENSE_T;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_PERSONAL_T> AG_PERSONAL_T
        {
            get
            {
                if ((_AG_PERSONAL_T == null))
                {
                    _AG_PERSONAL_T = new MockObjectSet<AG_PERSONAL_T>("AG_PERSONAL_T");
                }
                return _AG_PERSONAL_T;
            }
        }
        private IObjectSet<AG_PERSONAL_T> _AG_PERSONAL_T;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_TRAIN_PLAN_T> AG_TRAIN_PLAN_T
        {
            get
            {
                if ((_AG_TRAIN_PLAN_T == null))
                {
                    _AG_TRAIN_PLAN_T = new MockObjectSet<AG_TRAIN_PLAN_T>("AG_TRAIN_PLAN_T");
                }
                return _AG_TRAIN_PLAN_T;
            }
        }
        private IObjectSet<AG_TRAIN_PLAN_T> _AG_TRAIN_PLAN_T;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_TRAIN_SPECIAL_T> AG_TRAIN_SPECIAL_T
        {
            get
            {
                if ((_AG_TRAIN_SPECIAL_T == null))
                {
                    _AG_TRAIN_SPECIAL_T = new MockObjectSet<AG_TRAIN_SPECIAL_T>("AG_TRAIN_SPECIAL_T");
                }
                return _AG_TRAIN_SPECIAL_T;
            }
        }
        private IObjectSet<AG_TRAIN_SPECIAL_T> _AG_TRAIN_SPECIAL_T;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<VW_IAS_AMPUR> VW_IAS_AMPUR
        {
            get
            {
                if ((_VW_IAS_AMPUR == null))
                {
                    _VW_IAS_AMPUR = new MockObjectSet<VW_IAS_AMPUR>("VW_IAS_AMPUR");
                }
                return _VW_IAS_AMPUR;
            }
        }
        private IObjectSet<VW_IAS_AMPUR> _VW_IAS_AMPUR;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<VW_IAS_COM_CODE> VW_IAS_COM_CODE
        {
            get
            {
                if ((_VW_IAS_COM_CODE == null))
                {
                    _VW_IAS_COM_CODE = new MockObjectSet<VW_IAS_COM_CODE>("VW_IAS_COM_CODE");
                }
                return _VW_IAS_COM_CODE;
            }
        }
        private IObjectSet<VW_IAS_COM_CODE> _VW_IAS_COM_CODE;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<VW_IAS_PROVINCE> VW_IAS_PROVINCE
        {
            get
            {
                if ((_VW_IAS_PROVINCE == null))
                {
                    _VW_IAS_PROVINCE = new MockObjectSet<VW_IAS_PROVINCE>("VW_IAS_PROVINCE");
                }
                return _VW_IAS_PROVINCE;
            }
        }
        private IObjectSet<VW_IAS_PROVINCE> _VW_IAS_PROVINCE;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<VW_IAS_TITLE_NAME> VW_IAS_TITLE_NAME
        {
            get
            {
                if ((_VW_IAS_TITLE_NAME == null))
                {
                    _VW_IAS_TITLE_NAME = new MockObjectSet<VW_IAS_TITLE_NAME>("VW_IAS_TITLE_NAME");
                }
                return _VW_IAS_TITLE_NAME;
            }
        }
        private IObjectSet<VW_IAS_TITLE_NAME> _VW_IAS_TITLE_NAME;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<VW_IAS_TUMBON> VW_IAS_TUMBON
        {
            get
            {
                if ((_VW_IAS_TUMBON == null))
                {
                    _VW_IAS_TUMBON = new MockObjectSet<VW_IAS_TUMBON>("VW_IAS_TUMBON");
                }
                return _VW_IAS_TUMBON;
            }
        }
        private IObjectSet<VW_IAS_TUMBON> _VW_IAS_TUMBON;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_STATUS> AG_IAS_STATUS
        {
            get
            {
                if ((_AG_IAS_STATUS == null))
                {
                    _AG_IAS_STATUS = new MockObjectSet<AG_IAS_STATUS>("AG_IAS_STATUS");
                }
                return _AG_IAS_STATUS;
            }
        }
        private IObjectSet<AG_IAS_STATUS> _AG_IAS_STATUS;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_LICENSE_RENEW_T> AG_LICENSE_RENEW_T
        {
            get
            {
                if ((_AG_LICENSE_RENEW_T == null))
                {
                    _AG_LICENSE_RENEW_T = new MockObjectSet<AG_LICENSE_RENEW_T>("AG_LICENSE_RENEW_T");
                }
                return _AG_LICENSE_RENEW_T;
            }
        }
        private IObjectSet<AG_LICENSE_RENEW_T> _AG_LICENSE_RENEW_T;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_LICENSE_H_TEMP> AG_IAS_LICENSE_H_TEMP
        {
            get
            {
                if ((_AG_IAS_LICENSE_H_TEMP == null))
                {
                    _AG_IAS_LICENSE_H_TEMP = new MockObjectSet<AG_IAS_LICENSE_H_TEMP>("AG_IAS_LICENSE_H_TEMP");
                }
                return _AG_IAS_LICENSE_H_TEMP;
            }
        }
        private IObjectSet<AG_IAS_LICENSE_H_TEMP> _AG_IAS_LICENSE_H_TEMP;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_APPLICANT_SCORE_H_TEMP> AG_IAS_APPLICANT_SCORE_H_TEMP
        {
            get
            {
                if ((_AG_IAS_APPLICANT_SCORE_H_TEMP == null))
                {
                    _AG_IAS_APPLICANT_SCORE_H_TEMP = new MockObjectSet<AG_IAS_APPLICANT_SCORE_H_TEMP>("AG_IAS_APPLICANT_SCORE_H_TEMP");
                }
                return _AG_IAS_APPLICANT_SCORE_H_TEMP;
            }
        }
        private IObjectSet<AG_IAS_APPLICANT_SCORE_H_TEMP> _AG_IAS_APPLICANT_SCORE_H_TEMP;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_INVOICE_T> AG_IAS_INVOICE_T
        {
            get
            {
                if ((_AG_IAS_INVOICE_T == null))
                {
                    _AG_IAS_INVOICE_T = new MockObjectSet<AG_IAS_INVOICE_T>("AG_IAS_INVOICE_T");
                }
                return _AG_IAS_INVOICE_T;
            }
        }
        private IObjectSet<AG_IAS_INVOICE_T> _AG_IAS_INVOICE_T;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_PAYMENT_HEADER_T> AG_IAS_PAYMENT_HEADER_T
        {
            get
            {
                if ((_AG_IAS_PAYMENT_HEADER_T == null))
                {
                    _AG_IAS_PAYMENT_HEADER_T = new MockObjectSet<AG_IAS_PAYMENT_HEADER_T>("AG_IAS_PAYMENT_HEADER_T");
                }
                return _AG_IAS_PAYMENT_HEADER_T;
            }
        }
        private IObjectSet<AG_IAS_PAYMENT_HEADER_T> _AG_IAS_PAYMENT_HEADER_T;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_USERS_RIGHT> AG_IAS_USERS_RIGHT
        {
            get
            {
                if ((_AG_IAS_USERS_RIGHT == null))
                {
                    _AG_IAS_USERS_RIGHT = new MockObjectSet<AG_IAS_USERS_RIGHT>("AG_IAS_USERS_RIGHT");
                }
                return _AG_IAS_USERS_RIGHT;
            }
        }
        private IObjectSet<AG_IAS_USERS_RIGHT> _AG_IAS_USERS_RIGHT;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_TEMP_PAYMENT_HEADER> AG_IAS_TEMP_PAYMENT_HEADER
        {
            get
            {
                if ((_AG_IAS_TEMP_PAYMENT_HEADER == null))
                {
                    _AG_IAS_TEMP_PAYMENT_HEADER = new MockObjectSet<AG_IAS_TEMP_PAYMENT_HEADER>("AG_IAS_TEMP_PAYMENT_HEADER");
                }
                return _AG_IAS_TEMP_PAYMENT_HEADER;
            }
        }
        private IObjectSet<AG_IAS_TEMP_PAYMENT_HEADER> _AG_IAS_TEMP_PAYMENT_HEADER;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_TEMP_PAYMENT_DETAIL> AG_IAS_TEMP_PAYMENT_DETAIL
        {
            get
            {
                if ((_AG_IAS_TEMP_PAYMENT_DETAIL == null))
                {
                    _AG_IAS_TEMP_PAYMENT_DETAIL = new MockObjectSet<AG_IAS_TEMP_PAYMENT_DETAIL>("AG_IAS_TEMP_PAYMENT_DETAIL");
                }
                return _AG_IAS_TEMP_PAYMENT_DETAIL;
            }
        }
        private IObjectSet<AG_IAS_TEMP_PAYMENT_DETAIL> _AG_IAS_TEMP_PAYMENT_DETAIL;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_TEMP_PAYMENT_TOTAL> AG_IAS_TEMP_PAYMENT_TOTAL
        {
            get
            {
                if ((_AG_IAS_TEMP_PAYMENT_TOTAL == null))
                {
                    _AG_IAS_TEMP_PAYMENT_TOTAL = new MockObjectSet<AG_IAS_TEMP_PAYMENT_TOTAL>("AG_IAS_TEMP_PAYMENT_TOTAL");
                }
                return _AG_IAS_TEMP_PAYMENT_TOTAL;
            }
        }
        private IObjectSet<AG_IAS_TEMP_PAYMENT_TOTAL> _AG_IAS_TEMP_PAYMENT_TOTAL;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_BILL_CODE> AG_IAS_BILL_CODE
        {
            get
            {
                if ((_AG_IAS_BILL_CODE == null))
                {
                    _AG_IAS_BILL_CODE = new MockObjectSet<AG_IAS_BILL_CODE>("AG_IAS_BILL_CODE");
                }
                return _AG_IAS_BILL_CODE;
            }
        }
        private IObjectSet<AG_IAS_BILL_CODE> _AG_IAS_BILL_CODE;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_LICENSE_D_TEMP> AG_IAS_LICENSE_D_TEMP
        {
            get
            {
                if ((_AG_IAS_LICENSE_D_TEMP == null))
                {
                    _AG_IAS_LICENSE_D_TEMP = new MockObjectSet<AG_IAS_LICENSE_D_TEMP>("AG_IAS_LICENSE_D_TEMP");
                }
                return _AG_IAS_LICENSE_D_TEMP;
            }
        }
        private IObjectSet<AG_IAS_LICENSE_D_TEMP> _AG_IAS_LICENSE_D_TEMP;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_LOG_ACTIVITY> AG_IAS_LOG_ACTIVITY
        {
            get
            {
                if ((_AG_IAS_LOG_ACTIVITY == null))
                {
                    _AG_IAS_LOG_ACTIVITY = new MockObjectSet<AG_IAS_LOG_ACTIVITY>("AG_IAS_LOG_ACTIVITY");
                }
                return _AG_IAS_LOG_ACTIVITY;
            }
        }
        private IObjectSet<AG_IAS_LOG_ACTIVITY> _AG_IAS_LOG_ACTIVITY;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_ACCEPT_OFF_R> AG_ACCEPT_OFF_R
        {
            get
            {
                if ((_AG_ACCEPT_OFF_R == null))
                {
                    _AG_ACCEPT_OFF_R = new MockObjectSet<AG_ACCEPT_OFF_R>("AG_ACCEPT_OFF_R");
                }
                return _AG_ACCEPT_OFF_R;
            }
        }
        private IObjectSet<AG_ACCEPT_OFF_R> _AG_ACCEPT_OFF_R;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_LICENSE_TYPE_R> AG_LICENSE_TYPE_R
        {
            get
            {
                if ((_AG_LICENSE_TYPE_R == null))
                {
                    _AG_LICENSE_TYPE_R = new MockObjectSet<AG_LICENSE_TYPE_R>("AG_LICENSE_TYPE_R");
                }
                return _AG_LICENSE_TYPE_R;
            }
        }
        private IObjectSet<AG_LICENSE_TYPE_R> _AG_LICENSE_TYPE_R;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_PETITION_TYPE_R> AG_PETITION_TYPE_R
        {
            get
            {
                if ((_AG_PETITION_TYPE_R == null))
                {
                    _AG_PETITION_TYPE_R = new MockObjectSet<AG_PETITION_TYPE_R>("AG_PETITION_TYPE_R");
                }
                return _AG_PETITION_TYPE_R;
            }
        }
        private IObjectSet<AG_PETITION_TYPE_R> _AG_PETITION_TYPE_R;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_APPLICANT_DETAIL_TEMP> AG_IAS_APPLICANT_DETAIL_TEMP
        {
            get
            {
                if ((_AG_IAS_APPLICANT_DETAIL_TEMP == null))
                {
                    _AG_IAS_APPLICANT_DETAIL_TEMP = new MockObjectSet<AG_IAS_APPLICANT_DETAIL_TEMP>("AG_IAS_APPLICANT_DETAIL_TEMP");
                }
                return _AG_IAS_APPLICANT_DETAIL_TEMP;
            }
        }
        private IObjectSet<AG_IAS_APPLICANT_DETAIL_TEMP> _AG_IAS_APPLICANT_DETAIL_TEMP;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_ATTACH_FILE_LICENSE2> AG_IAS_ATTACH_FILE_LICENSE2
        {
            get
            {
                if ((_AG_IAS_ATTACH_FILE_LICENSE2 == null))
                {
                    _AG_IAS_ATTACH_FILE_LICENSE2 = new MockObjectSet<AG_IAS_ATTACH_FILE_LICENSE2>("AG_IAS_ATTACH_FILE_LICENSE2");
                }
                return _AG_IAS_ATTACH_FILE_LICENSE2;
            }
        }
        private IObjectSet<AG_IAS_ATTACH_FILE_LICENSE2> _AG_IAS_ATTACH_FILE_LICENSE2;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_IMPORT_HEADER_TEMP> AG_IAS_IMPORT_HEADER_TEMP
        {
            get
            {
                if ((_AG_IAS_IMPORT_HEADER_TEMP == null))
                {
                    _AG_IAS_IMPORT_HEADER_TEMP = new MockObjectSet<AG_IAS_IMPORT_HEADER_TEMP>("AG_IAS_IMPORT_HEADER_TEMP");
                }
                return _AG_IAS_IMPORT_HEADER_TEMP;
            }
        }
        private IObjectSet<AG_IAS_IMPORT_HEADER_TEMP> _AG_IAS_IMPORT_HEADER_TEMP;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_IMPORT_DETAIL_TEMP> AG_IAS_IMPORT_DETAIL_TEMP
        {
            get
            {
                if ((_AG_IAS_IMPORT_DETAIL_TEMP == null))
                {
                    _AG_IAS_IMPORT_DETAIL_TEMP = new MockObjectSet<AG_IAS_IMPORT_DETAIL_TEMP>("AG_IAS_IMPORT_DETAIL_TEMP");
                }
                return _AG_IAS_IMPORT_DETAIL_TEMP;
            }
        }
        private IObjectSet<AG_IAS_IMPORT_DETAIL_TEMP> _AG_IAS_IMPORT_DETAIL_TEMP;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_ATTACH_FILE_LICENSE> AG_IAS_ATTACH_FILE_LICENSE
        {
            get
            {
                if ((_AG_IAS_ATTACH_FILE_LICENSE == null))
                {
                    _AG_IAS_ATTACH_FILE_LICENSE = new MockObjectSet<AG_IAS_ATTACH_FILE_LICENSE>("AG_IAS_ATTACH_FILE_LICENSE");
                }
                return _AG_IAS_ATTACH_FILE_LICENSE;
            }
        }
        private IObjectSet<AG_IAS_ATTACH_FILE_LICENSE> _AG_IAS_ATTACH_FILE_LICENSE;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_DOCUMENT_TYPE> AG_IAS_DOCUMENT_TYPE
        {
            get
            {
                if ((_AG_IAS_DOCUMENT_TYPE == null))
                {
                    _AG_IAS_DOCUMENT_TYPE = new MockObjectSet<AG_IAS_DOCUMENT_TYPE>("AG_IAS_DOCUMENT_TYPE");
                }
                return _AG_IAS_DOCUMENT_TYPE;
            }
        }
        private IObjectSet<AG_IAS_DOCUMENT_TYPE> _AG_IAS_DOCUMENT_TYPE;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_PETITION_TYPE_R> AG_IAS_PETITION_TYPE_R
        {
            get
            {
                if ((_AG_IAS_PETITION_TYPE_R == null))
                {
                    _AG_IAS_PETITION_TYPE_R = new MockObjectSet<AG_IAS_PETITION_TYPE_R>("AG_IAS_PETITION_TYPE_R");
                }
                return _AG_IAS_PETITION_TYPE_R;
            }
        }
        private IObjectSet<AG_IAS_PETITION_TYPE_R> _AG_IAS_PETITION_TYPE_R;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_PAYMENT_G_T> AG_IAS_PAYMENT_G_T
        {
            get
            {
                if ((_AG_IAS_PAYMENT_G_T == null))
                {
                    _AG_IAS_PAYMENT_G_T = new MockObjectSet<AG_IAS_PAYMENT_G_T>("AG_IAS_PAYMENT_G_T");
                }
                return _AG_IAS_PAYMENT_G_T;
            }
        }
        private IObjectSet<AG_IAS_PAYMENT_G_T> _AG_IAS_PAYMENT_G_T;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_PAYMENT_RUNNINGNO> AG_IAS_PAYMENT_RUNNINGNO
        {
            get
            {
                if ((_AG_IAS_PAYMENT_RUNNINGNO == null))
                {
                    _AG_IAS_PAYMENT_RUNNINGNO = new MockObjectSet<AG_IAS_PAYMENT_RUNNINGNO>("AG_IAS_PAYMENT_RUNNINGNO");
                }
                return _AG_IAS_PAYMENT_RUNNINGNO;
            }
        }
        private IObjectSet<AG_IAS_PAYMENT_RUNNINGNO> _AG_IAS_PAYMENT_RUNNINGNO;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_LICENSE_TYPE_R> AG_IAS_LICENSE_TYPE_R
        {
            get
            {
                if ((_AG_IAS_LICENSE_TYPE_R == null))
                {
                    _AG_IAS_LICENSE_TYPE_R = new MockObjectSet<AG_IAS_LICENSE_TYPE_R>("AG_IAS_LICENSE_TYPE_R");
                }
                return _AG_IAS_LICENSE_TYPE_R;
            }
        }
        private IObjectSet<AG_IAS_LICENSE_TYPE_R> _AG_IAS_LICENSE_TYPE_R;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<VW_AS_COMPANY_T_BC1> VW_AS_COMPANY_T_BC1
        {
            get
            {
                if ((_VW_AS_COMPANY_T_BC1 == null))
                {
                    _VW_AS_COMPANY_T_BC1 = new MockObjectSet<VW_AS_COMPANY_T_BC1>("VW_AS_COMPANY_T_BC1");
                }
                return _VW_AS_COMPANY_T_BC1;
            }
        }
        private IObjectSet<VW_AS_COMPANY_T_BC1> _VW_AS_COMPANY_T_BC1;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<VW_AS_COMPANY_T_BC2> VW_AS_COMPANY_T_BC2
        {
            get
            {
                if ((_VW_AS_COMPANY_T_BC2 == null))
                {
                    _VW_AS_COMPANY_T_BC2 = new MockObjectSet<VW_AS_COMPANY_T_BC2>("VW_AS_COMPANY_T_BC2");
                }
                return _VW_AS_COMPANY_T_BC2;
            }
        }
        private IObjectSet<VW_AS_COMPANY_T_BC2> _VW_AS_COMPANY_T_BC2;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<VW_AS_BUSI_TYPE_R> VW_AS_BUSI_TYPE_R
        {
            get
            {
                if ((_VW_AS_BUSI_TYPE_R == null))
                {
                    _VW_AS_BUSI_TYPE_R = new MockObjectSet<VW_AS_BUSI_TYPE_R>("VW_AS_BUSI_TYPE_R");
                }
                return _VW_AS_BUSI_TYPE_R;
            }
        }
        private IObjectSet<VW_AS_BUSI_TYPE_R> _VW_AS_BUSI_TYPE_R;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<VW_AS_COMPANY_T> VW_AS_COMPANY_T
        {
            get
            {
                if ((_VW_AS_COMPANY_T == null))
                {
                    _VW_AS_COMPANY_T = new MockObjectSet<VW_AS_COMPANY_T>("VW_AS_COMPANY_T");
                }
                return _VW_AS_COMPANY_T;
            }
        }
        private IObjectSet<VW_AS_COMPANY_T> _VW_AS_COMPANY_T;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_TRAIN_T> AG_TRAIN_T
        {
            get
            {
                if ((_AG_TRAIN_T == null))
                {
                    _AG_TRAIN_T = new MockObjectSet<AG_TRAIN_T>("AG_TRAIN_T");
                }
                return _AG_TRAIN_T;
            }
        }
        private IObjectSet<AG_TRAIN_T> _AG_TRAIN_T;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_U_TRAIN_T> AG_U_TRAIN_T
        {
            get
            {
                if ((_AG_U_TRAIN_T == null))
                {
                    _AG_U_TRAIN_T = new MockObjectSet<AG_U_TRAIN_T>("AG_U_TRAIN_T");
                }
                return _AG_U_TRAIN_T;
            }
        }
        private IObjectSet<AG_U_TRAIN_T> _AG_U_TRAIN_T;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_PERSON_INCOMP_AGENT_T> AG_PERSON_INCOMP_AGENT_T
        {
            get
            {
                if ((_AG_PERSON_INCOMP_AGENT_T == null))
                {
                    _AG_PERSON_INCOMP_AGENT_T = new MockObjectSet<AG_PERSON_INCOMP_AGENT_T>("AG_PERSON_INCOMP_AGENT_T");
                }
                return _AG_PERSON_INCOMP_AGENT_T;
            }
        }
        private IObjectSet<AG_PERSON_INCOMP_AGENT_T> _AG_PERSON_INCOMP_AGENT_T;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_LICENSE_H> AG_IAS_LICENSE_H
        {
            get
            {
                if ((_AG_IAS_LICENSE_H == null))
                {
                    _AG_IAS_LICENSE_H = new MockObjectSet<AG_IAS_LICENSE_H>("AG_IAS_LICENSE_H");
                }
                return _AG_IAS_LICENSE_H;
            }
        }
        private IObjectSet<AG_IAS_LICENSE_H> _AG_IAS_LICENSE_H;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_SUBPAYMENT_H_T> AG_IAS_SUBPAYMENT_H_T
        {
            get
            {
                if ((_AG_IAS_SUBPAYMENT_H_T == null))
                {
                    _AG_IAS_SUBPAYMENT_H_T = new MockObjectSet<AG_IAS_SUBPAYMENT_H_T>("AG_IAS_SUBPAYMENT_H_T");
                }
                return _AG_IAS_SUBPAYMENT_H_T;
            }
        }
        private IObjectSet<AG_IAS_SUBPAYMENT_H_T> _AG_IAS_SUBPAYMENT_H_T;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_LICENSE_D> AG_IAS_LICENSE_D
        {
            get
            {
                if ((_AG_IAS_LICENSE_D == null))
                {
                    _AG_IAS_LICENSE_D = new MockObjectSet<AG_IAS_LICENSE_D>("AG_IAS_LICENSE_D");
                }
                return _AG_IAS_LICENSE_D;
            }
        }
        private IObjectSet<AG_IAS_LICENSE_D> _AG_IAS_LICENSE_D;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_DOCUMENT_TYPE_T> AG_IAS_DOCUMENT_TYPE_T
        {
            get
            {
                if ((_AG_IAS_DOCUMENT_TYPE_T == null))
                {
                    _AG_IAS_DOCUMENT_TYPE_T = new MockObjectSet<AG_IAS_DOCUMENT_TYPE_T>("AG_IAS_DOCUMENT_TYPE_T");
                }
                return _AG_IAS_DOCUMENT_TYPE_T;
            }
        }
        private IObjectSet<AG_IAS_DOCUMENT_TYPE_T> _AG_IAS_DOCUMENT_TYPE_T;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_APPLICANT_SCORE_D_TEMP> AG_IAS_APPLICANT_SCORE_D_TEMP
        {
            get
            {
                if ((_AG_IAS_APPLICANT_SCORE_D_TEMP == null))
                {
                    _AG_IAS_APPLICANT_SCORE_D_TEMP = new MockObjectSet<AG_IAS_APPLICANT_SCORE_D_TEMP>("AG_IAS_APPLICANT_SCORE_D_TEMP");
                }
                return _AG_IAS_APPLICANT_SCORE_D_TEMP;
            }
        }
        private IObjectSet<AG_IAS_APPLICANT_SCORE_D_TEMP> _AG_IAS_APPLICANT_SCORE_D_TEMP;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_AGENT_T> AG_AGENT_T
        {
            get
            {
                if ((_AG_AGENT_T == null))
                {
                    _AG_AGENT_T = new MockObjectSet<AG_AGENT_T>("AG_AGENT_T");
                }
                return _AG_AGENT_T;
            }
        }
        private IObjectSet<AG_AGENT_T> _AG_AGENT_T;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_HIS_MOVE_COMP_AGENT_T> AG_HIS_MOVE_COMP_AGENT_T
        {
            get
            {
                if ((_AG_HIS_MOVE_COMP_AGENT_T == null))
                {
                    _AG_HIS_MOVE_COMP_AGENT_T = new MockObjectSet<AG_HIS_MOVE_COMP_AGENT_T>("AG_HIS_MOVE_COMP_AGENT_T");
                }
                return _AG_HIS_MOVE_COMP_AGENT_T;
            }
        }
        private IObjectSet<AG_HIS_MOVE_COMP_AGENT_T> _AG_HIS_MOVE_COMP_AGENT_T;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_REGISTRATION_T> AG_IAS_REGISTRATION_T
        {
            get
            {
                if ((_AG_IAS_REGISTRATION_T == null))
                {
                    _AG_IAS_REGISTRATION_T = new MockObjectSet<AG_IAS_REGISTRATION_T>("AG_IAS_REGISTRATION_T");
                }
                return _AG_IAS_REGISTRATION_T;
            }
        }
        private IObjectSet<AG_IAS_REGISTRATION_T> _AG_IAS_REGISTRATION_T;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_AGENT_TYPE_R> AG_AGENT_TYPE_R
        {
            get
            {
                if ((_AG_AGENT_TYPE_R == null))
                {
                    _AG_AGENT_TYPE_R = new MockObjectSet<AG_AGENT_TYPE_R>("AG_AGENT_TYPE_R");
                }
                return _AG_AGENT_TYPE_R;
            }
        }
        private IObjectSet<AG_AGENT_TYPE_R> _AG_AGENT_TYPE_R;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_PAYMENT_EXPIRE_DAY> AG_IAS_PAYMENT_EXPIRE_DAY
        {
            get
            {
                if ((_AG_IAS_PAYMENT_EXPIRE_DAY == null))
                {
                    _AG_IAS_PAYMENT_EXPIRE_DAY = new MockObjectSet<AG_IAS_PAYMENT_EXPIRE_DAY>("AG_IAS_PAYMENT_EXPIRE_DAY");
                }
                return _AG_IAS_PAYMENT_EXPIRE_DAY;
            }
        }
        private IObjectSet<AG_IAS_PAYMENT_EXPIRE_DAY> _AG_IAS_PAYMENT_EXPIRE_DAY;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_RECEIPT_HISTORY> AG_IAS_RECEIPT_HISTORY
        {
            get
            {
                if ((_AG_IAS_RECEIPT_HISTORY == null))
                {
                    _AG_IAS_RECEIPT_HISTORY = new MockObjectSet<AG_IAS_RECEIPT_HISTORY>("AG_IAS_RECEIPT_HISTORY");
                }
                return _AG_IAS_RECEIPT_HISTORY;
            }
        }
        private IObjectSet<AG_IAS_RECEIPT_HISTORY> _AG_IAS_RECEIPT_HISTORY;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<VW_IAS_TITLE_NAME_PRIORITY> VW_IAS_TITLE_NAME_PRIORITY
        {
            get
            {
                if ((_VW_IAS_TITLE_NAME_PRIORITY == null))
                {
                    _VW_IAS_TITLE_NAME_PRIORITY = new MockObjectSet<VW_IAS_TITLE_NAME_PRIORITY>("VW_IAS_TITLE_NAME_PRIORITY");
                }
                return _VW_IAS_TITLE_NAME_PRIORITY;
            }
        }
        private IObjectSet<VW_IAS_TITLE_NAME_PRIORITY> _VW_IAS_TITLE_NAME_PRIORITY;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_USERS> AG_IAS_USERS
        {
            get
            {
                if ((_AG_IAS_USERS == null))
                {
                    _AG_IAS_USERS = new MockObjectSet<AG_IAS_USERS>("AG_IAS_USERS");
                }
                return _AG_IAS_USERS;
            }
        }
        private IObjectSet<AG_IAS_USERS> _AG_IAS_USERS;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_PAYMENT_T> AG_IAS_PAYMENT_T
        {
            get
            {
                if ((_AG_IAS_PAYMENT_T == null))
                {
                    _AG_IAS_PAYMENT_T = new MockObjectSet<AG_IAS_PAYMENT_T>("AG_IAS_PAYMENT_T");
                }
                return _AG_IAS_PAYMENT_T;
            }
        }
        private IObjectSet<AG_IAS_PAYMENT_T> _AG_IAS_PAYMENT_T;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_PERSONAL_T> AG_IAS_PERSONAL_T
        {
            get
            {
                if ((_AG_IAS_PERSONAL_T == null))
                {
                    _AG_IAS_PERSONAL_T = new MockObjectSet<AG_IAS_PERSONAL_T>("AG_IAS_PERSONAL_T");
                }
                return _AG_IAS_PERSONAL_T;
            }
        }
        private IObjectSet<AG_IAS_PERSONAL_T> _AG_IAS_PERSONAL_T;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_TRAIN_PERSON_T> AG_TRAIN_PERSON_T
        {
            get
            {
                if ((_AG_TRAIN_PERSON_T == null))
                {
                    _AG_TRAIN_PERSON_T = new MockObjectSet<AG_TRAIN_PERSON_T>("AG_TRAIN_PERSON_T");
                }
                return _AG_TRAIN_PERSON_T;
            }
        }
        private IObjectSet<AG_TRAIN_PERSON_T> _AG_TRAIN_PERSON_T;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_TRAIN_SPECIAL_R> AG_TRAIN_SPECIAL_R
        {
            get
            {
                if ((_AG_TRAIN_SPECIAL_R == null))
                {
                    _AG_TRAIN_SPECIAL_R = new MockObjectSet<AG_TRAIN_SPECIAL_R>("AG_TRAIN_SPECIAL_R");
                }
                return _AG_TRAIN_SPECIAL_R;
            }
        }
        private IObjectSet<AG_TRAIN_SPECIAL_R> _AG_TRAIN_SPECIAL_R;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_SUBPAYMENT_D_T> AG_IAS_SUBPAYMENT_D_T
        {
            get
            {
                if ((_AG_IAS_SUBPAYMENT_D_T == null))
                {
                    _AG_IAS_SUBPAYMENT_D_T = new MockObjectSet<AG_IAS_SUBPAYMENT_D_T>("AG_IAS_SUBPAYMENT_D_T");
                }
                return _AG_IAS_SUBPAYMENT_D_T;
            }
        }
        private IObjectSet<AG_IAS_SUBPAYMENT_D_T> _AG_IAS_SUBPAYMENT_D_T;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_SUBJECT_R> AG_SUBJECT_R
        {
            get
            {
                if ((_AG_SUBJECT_R == null))
                {
                    _AG_SUBJECT_R = new MockObjectSet<AG_SUBJECT_R>("AG_SUBJECT_R");
                }
                return _AG_SUBJECT_R;
            }
        }
        private IObjectSet<AG_SUBJECT_R> _AG_SUBJECT_R;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_ASSOCIATION> AG_IAS_ASSOCIATION
        {
            get
            {
                if ((_AG_IAS_ASSOCIATION == null))
                {
                    _AG_IAS_ASSOCIATION = new MockObjectSet<AG_IAS_ASSOCIATION>("AG_IAS_ASSOCIATION");
                }
                return _AG_IAS_ASSOCIATION;
            }
        }
        private IObjectSet<AG_IAS_ASSOCIATION> _AG_IAS_ASSOCIATION;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_EXAM_PLACE_ROOM> AG_IAS_EXAM_ROOM
        {
            get
            {
                if ((_AG_IAS_EXAM_ROOM == null))
                {
                    _AG_IAS_EXAM_ROOM = new MockObjectSet<AG_IAS_EXAM_PLACE_ROOM>("AG_IAS_EXAM_ROOM");
                }
                return _AG_IAS_EXAM_ROOM;
            }
        }
        private IObjectSet<AG_IAS_EXAM_PLACE_ROOM> _AG_IAS_EXAM_ROOM;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_HIST_PERSONAL_T> AG_IAS_HIST_PERSONAL_T
        {
            get
            {
                if ((_AG_IAS_HIST_PERSONAL_T == null))
                {
                    _AG_IAS_HIST_PERSONAL_T = new MockObjectSet<AG_IAS_HIST_PERSONAL_T>("AG_IAS_HIST_PERSONAL_T");
                }
                return _AG_IAS_HIST_PERSONAL_T;
            }
        }
        private IObjectSet<AG_IAS_HIST_PERSONAL_T> _AG_IAS_HIST_PERSONAL_T;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_TEMP_PERSONAL_T> AG_IAS_TEMP_PERSONAL_T
        {
            get
            {
                if ((_AG_IAS_TEMP_PERSONAL_T == null))
                {
                    _AG_IAS_TEMP_PERSONAL_T = new MockObjectSet<AG_IAS_TEMP_PERSONAL_T>("AG_IAS_TEMP_PERSONAL_T");
                }
                return _AG_IAS_TEMP_PERSONAL_T;
            }
        }
        private IObjectSet<AG_IAS_TEMP_PERSONAL_T> _AG_IAS_TEMP_PERSONAL_T;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_EXAM_ROOM_LICENSE_R> AG_IAS_EXAM_ROOM_LICENSE_R
        {
            get
            {
                if ((_AG_IAS_EXAM_ROOM_LICENSE_R == null))
                {
                    _AG_IAS_EXAM_ROOM_LICENSE_R = new MockObjectSet<AG_IAS_EXAM_ROOM_LICENSE_R>("AG_IAS_EXAM_ROOM_LICENSE_R");
                }
                return _AG_IAS_EXAM_ROOM_LICENSE_R;
            }
        }
        private IObjectSet<AG_IAS_EXAM_ROOM_LICENSE_R> _AG_IAS_EXAM_ROOM_LICENSE_R;


        public IObjectSet<AG_IAS_CONFIG> AG_IAS_CONFIG
        {
            get
            {
                if ((_AG_IAS_CONFIG == null))
                {
                    _AG_IAS_CONFIG = new MockObjectSet<AG_IAS_CONFIG>("AG_IAS_EXAM_ROOM_LICENSE_R");
                }
                return _AG_IAS_CONFIG;
            }
        }
        private IObjectSet<AG_IAS_CONFIG> _AG_IAS_CONFIG;



        public IObjectSet<AG_IAS_EXAM_PLACE_ROOM> AG_IAS_EXAM_PLACE_ROOM
        {
            get
            {
                if ((_AG_IAS_EXAM_PLACE_ROOM == null))
                {
                    _AG_IAS_EXAM_PLACE_ROOM = new MockObjectSet<AG_IAS_EXAM_PLACE_ROOM>("AG_IAS_EXAM_PLACE_ROOM");
                }
                return _AG_IAS_EXAM_PLACE_ROOM;
            }
        }
        private IObjectSet<AG_IAS_EXAM_PLACE_ROOM> _AG_IAS_EXAM_PLACE_ROOM;

        public IObjectSet<AG_IAS_ASSOCIATION_LICENSE> AG_IAS_ASSOCIATION_LICENSE
        {
            get
            {
                if ((_AG_IAS_ASSOCIATION_LICENSE == null))
                {
                    _AG_IAS_ASSOCIATION_LICENSE = new MockObjectSet<AG_IAS_ASSOCIATION_LICENSE>("AG_IAS_ASSOCIATION_LICENSE");
                }
                return _AG_IAS_ASSOCIATION_LICENSE;
            }
        }
        private IObjectSet<AG_IAS_ASSOCIATION_LICENSE> _AG_IAS_ASSOCIATION_LICENSE;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_EXAM_CONDITION_R> AG_EXAM_CONDITION_R
        {
            get
            {
                if ((_AG_EXAM_CONDITION_R == null))
                {
                    _AG_EXAM_CONDITION_R = new MockObjectSet<AG_EXAM_CONDITION_R>("AG_EXAM_CONDITION_R");
                }
                return _AG_EXAM_CONDITION_R;
            }
        }
        private IObjectSet<AG_EXAM_CONDITION_R> _AG_EXAM_CONDITION_R;


        public IObjectSet<AG_IAS_ATTACH_FILE_APPLICANT> AG_IAS_ATTACH_FILE_APPLICANT
        {
            get
            {
                if ((_AG_IAS_ATTACH_FILE_APPLICANT == null))
                {
                    _AG_IAS_ATTACH_FILE_APPLICANT = new MockObjectSet<AG_IAS_ATTACH_FILE_APPLICANT>("AG_IAS_ATTACH_FILE_APPLICANT");
                }
                return _AG_IAS_ATTACH_FILE_APPLICANT;
            }
        }

        private IObjectSet<AG_IAS_ATTACH_FILE_APPLICANT> _AG_IAS_ATTACH_FILE_APPLICANT;

        public IObjectSet<AG_IAS_APPLICANT_CHANGE> AG_IAS_APPLICANT_CHANGE
        {
            get
            {
                if ((_AG_IAS_APPLICANT_CHANGE == null))
                {
                    _AG_IAS_APPLICANT_CHANGE = new MockObjectSet<AG_IAS_APPLICANT_CHANGE>("AG_IAS_APPLICANT_CHANGE");
                }
                return _AG_IAS_APPLICANT_CHANGE;
            }
        }
        private IObjectSet<AG_IAS_APPLICANT_CHANGE> _AG_IAS_APPLICANT_CHANGE;

        public IObjectSet<AG_IAS_APPLICANT_T_LOG> AG_IAS_APPLICANT_T_LOG
        {
            get
            {
                if ((_AG_IAS_APPLICANT_T_LOG == null))
                {
                    _AG_IAS_APPLICANT_T_LOG = new MockObjectSet<AG_IAS_APPLICANT_T_LOG>("AG_IAS_APPLICANT_T_LOG");
                }
                return _AG_IAS_APPLICANT_T_LOG;
            }
        }
        private IObjectSet<AG_IAS_APPLICANT_T_LOG> _AG_IAS_APPLICANT_T_LOG;

        public IObjectSet<AG_IAS_VALIDATE_LICENSE> AG_IAS_VALIDATE_LICENSE
        {
            get
            {
                if ((_AG_IAS_VALIDATE_LICENSE == null))
                {
                    _AG_IAS_VALIDATE_LICENSE = new MockObjectSet<AG_IAS_VALIDATE_LICENSE>("AG_IAS_VALIDATE_LICENSE");
                }
                return _AG_IAS_VALIDATE_LICENSE;
            }
        }
        private IObjectSet<AG_IAS_VALIDATE_LICENSE> _AG_IAS_VALIDATE_LICENSE;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_VALIDATE_LICENSE_CON> AG_IAS_VALIDATE_LICENSE_CON
        {
            get
            {
                if ((_AG_IAS_VALIDATE_LICENSE_CON == null))
                {
                    _AG_IAS_VALIDATE_LICENSE_CON = new MockObjectSet<AG_IAS_VALIDATE_LICENSE_CON>("AG_IAS_VALIDATE_LICENSE_CON");
                }
                return _AG_IAS_VALIDATE_LICENSE_CON;
            }
        }
        private IObjectSet<AG_IAS_VALIDATE_LICENSE_CON> _AG_IAS_VALIDATE_LICENSE_CON;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_VALIDATE_LICENSE_GROUP> AG_IAS_VALIDATE_LICENSE_GROUP
        {
            get
            {
                if ((_AG_IAS_VALIDATE_LICENSE_GROUP == null))
                {
                    _AG_IAS_VALIDATE_LICENSE_GROUP = new MockObjectSet<AG_IAS_VALIDATE_LICENSE_GROUP>("AG_IAS_VALIDATE_LICENSE_GROUP");
                }
                return _AG_IAS_VALIDATE_LICENSE_GROUP;
            }
        }
        private IObjectSet<AG_IAS_VALIDATE_LICENSE_GROUP> _AG_IAS_VALIDATE_LICENSE_GROUP;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_SUBJECT_GROUP> AG_IAS_SUBJECT_GROUP
        {
            get
            {
                if ((_AG_IAS_SUBJECT_GROUP == null))
                {
                    _AG_IAS_SUBJECT_GROUP = new MockObjectSet<AG_IAS_SUBJECT_GROUP>("AG_IAS_SUBJECT_GROUP");
                }
                return _AG_IAS_SUBJECT_GROUP;
            }
        }
        private IObjectSet<AG_IAS_SUBJECT_GROUP> _AG_IAS_SUBJECT_GROUP;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_EXAM_CONDITION_GROUP> AG_IAS_EXAM_CONDITION_GROUP
        {
            get
            {
                if ((_AG_IAS_EXAM_CONDITION_GROUP == null))
                {
                    _AG_IAS_EXAM_CONDITION_GROUP = new MockObjectSet<AG_IAS_EXAM_CONDITION_GROUP>("AG_IAS_EXAM_CONDITION_GROUP");
                }
                return _AG_IAS_EXAM_CONDITION_GROUP;
            }
        }
        private IObjectSet<AG_IAS_EXAM_CONDITION_GROUP> _AG_IAS_EXAM_CONDITION_GROUP;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_EXAM_CONDITION_GROUP_D> AG_IAS_EXAM_CONDITION_GROUP_D
        {
            get
            {
                if ((_AG_IAS_EXAM_CONDITION_GROUP_D == null))
                {
                    _AG_IAS_EXAM_CONDITION_GROUP_D = new MockObjectSet<AG_IAS_EXAM_CONDITION_GROUP_D>("AG_IAS_EXAM_CONDITION_GROUP_D");
                }
                return _AG_IAS_EXAM_CONDITION_GROUP_D;
            }
        }
        private IObjectSet<AG_IAS_EXAM_CONDITION_GROUP_D> _AG_IAS_EXAM_CONDITION_GROUP_D;


        public IObjectSet<AG_IAS_APPROVE_FIELD> AG_IAS_APPROVE_FIELD
        {
            get
            {
                if ((_AG_IAS_APPROVE_FIELD == null))
                {
                    _AG_IAS_APPROVE_FIELD = new MockObjectSet<AG_IAS_APPROVE_FIELD>("AG_IAS_APPROVE_FIELD");
                }
                return _AG_IAS_APPROVE_FIELD;
            }
        }
        private IObjectSet<AG_IAS_APPROVE_FIELD> _AG_IAS_APPROVE_FIELD;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_ASSOCIATION_APPROVE> AG_IAS_ASSOCIATION_APPROVE
        {
            get
            {
                if ((_AG_IAS_ASSOCIATION_APPROVE == null))
                {
                    _AG_IAS_ASSOCIATION_APPROVE = new MockObjectSet<AG_IAS_ASSOCIATION_APPROVE>("AG_IAS_ASSOCIATION_APPROVE");
                }
                return _AG_IAS_ASSOCIATION_APPROVE;
            }
        }
        private IObjectSet<AG_IAS_ASSOCIATION_APPROVE> _AG_IAS_ASSOCIATION_APPROVE;


        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_EXAM_SPECIAL_R> AG_IAS_EXAM_SPECIAL_R
        {
            get
            {
                if ((_AG_IAS_EXAM_SPECIAL_R == null))
                {
                    _AG_IAS_EXAM_SPECIAL_R = new MockObjectSet<AG_IAS_EXAM_SPECIAL_R>("AG_IAS_EXAM_SPECIAL_R");
                }
                return _AG_IAS_EXAM_SPECIAL_R;
            }
        }
        private IObjectSet<AG_IAS_EXAM_SPECIAL_R> _AG_IAS_EXAM_SPECIAL_R;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_EXAM_SPECIAL_T> AG_IAS_EXAM_SPECIAL_T
        {
            get
            {
                if ((_AG_IAS_EXAM_SPECIAL_T == null))
                {
                    _AG_IAS_EXAM_SPECIAL_T = new MockObjectSet<AG_IAS_EXAM_SPECIAL_T>("AG_IAS_EXAM_SPECIAL_T");
                }
                return _AG_IAS_EXAM_SPECIAL_T;
            }
        }
        private IObjectSet<AG_IAS_EXAM_SPECIAL_T> _AG_IAS_EXAM_SPECIAL_T;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_SPECIAL_T_TEMP> AG_IAS_SPECIAL_T_TEMP
        {
            get
            {
                if ((_AG_IAS_SPECIAL_T_TEMP == null))
                {
                    _AG_IAS_SPECIAL_T_TEMP = new MockObjectSet<AG_IAS_SPECIAL_T_TEMP>("AG_IAS_SPECIAL_T_TEMP");
                }
                return _AG_IAS_SPECIAL_T_TEMP;
            }
        }
        private IObjectSet<AG_IAS_SPECIAL_T_TEMP> _AG_IAS_SPECIAL_T_TEMP;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_EXAM_SUBJECT_GROUP> AG_IAS_EXAM_SUBJECT_GROUP
        {
            get
            {
                if ((_AG_IAS_EXAM_SUBJECT_GROUP == null))
                {
                    _AG_IAS_EXAM_SUBJECT_GROUP = new MockObjectSet<AG_IAS_EXAM_SUBJECT_GROUP>("AG_IAS_EXAM_SUBJECT_GROUP");
                }
                return _AG_IAS_EXAM_SUBJECT_GROUP;
            }
        }
        private IObjectSet<AG_IAS_EXAM_SUBJECT_GROUP> _AG_IAS_EXAM_SUBJECT_GROUP;



        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_TRAIN_HOUR_T> AG_IAS_TRAIN_HOUR_T
        {
            get
            {
                if ((_AG_IAS_TRAIN_HOUR_T == null))
                {
                    _AG_IAS_TRAIN_HOUR_T = new MockObjectSet<AG_IAS_TRAIN_HOUR_T>("AG_IAS_TRAIN_HOUR_T");
                }
                return _AG_IAS_TRAIN_HOUR_T;
            }
        }
        private IObjectSet<AG_IAS_TRAIN_HOUR_T> _AG_IAS_TRAIN_HOUR_T;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_APPLICANT_ROOM> AG_IAS_APPLICANT_ROOM
        {
            get
            {
                if ((_AG_IAS_APPLICANT_ROOM == null))
                {
                    _AG_IAS_APPLICANT_ROOM = new MockObjectSet<AG_IAS_APPLICANT_ROOM>("AG_IAS_APPLICANT_ROOM");
                }
                return _AG_IAS_APPLICANT_ROOM;
            }
        }
        private IObjectSet<AG_IAS_APPLICANT_ROOM> _AG_IAS_APPLICANT_ROOM;


        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_TEMP_PAYMENT_DETAIL_HIS> AG_IAS_TEMP_PAYMENT_DETAIL_HIS
        {
            get
            {
                if ((_AG_IAS_TEMP_PAYMENT_DETAIL_HIS == null))
                {
                    _AG_IAS_TEMP_PAYMENT_DETAIL_HIS = new MockObjectSet<AG_IAS_TEMP_PAYMENT_DETAIL_HIS>("AG_IAS_TEMP_PAYMENT_DETAIL_HIS");
                }
                return _AG_IAS_TEMP_PAYMENT_DETAIL_HIS;
            }
        }
        private IObjectSet<AG_IAS_TEMP_PAYMENT_DETAIL_HIS> _AG_IAS_TEMP_PAYMENT_DETAIL_HIS;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<APPM_BUDGET_CODE> APPM_BUDGET_CODE
        {
            get
            {
                if ((_APPM_BUDGET_CODE == null))
                {
                    _APPM_BUDGET_CODE = new MockObjectSet<APPM_BUDGET_CODE>("APPM_BUDGET_CODE");
                }
                return _APPM_BUDGET_CODE;
            }
        }
        private IObjectSet<APPM_BUDGET_CODE> _APPM_BUDGET_CODE;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<APPM_RECEIVE_GROUP> APPM_RECEIVE_GROUP
        {
            get
            {
                if ((_APPM_RECEIVE_GROUP == null))
                {
                    _APPM_RECEIVE_GROUP = new MockObjectSet<APPM_RECEIVE_GROUP>("APPM_RECEIVE_GROUP");
                }
                return _APPM_RECEIVE_GROUP;
            }
        }
        private IObjectSet<APPM_RECEIVE_GROUP> _APPM_RECEIVE_GROUP;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_EXAM_APPLICANT> AG_IAS_EXAM_APPLICANT
        {
            get
            {
                if ((_AG_IAS_EXAM_APPLICANT == null))
                {
                    _AG_IAS_EXAM_APPLICANT = new MockObjectSet<AG_IAS_EXAM_APPLICANT>("AG_IAS_EXAM_APPLICANT");
                }
                return _AG_IAS_EXAM_APPLICANT;
            }
        }
        private IObjectSet<AG_IAS_EXAM_APPLICANT> _AG_IAS_EXAM_APPLICANT;


        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_PAYMENT_FILE> AG_IAS_PAYMENT_FILE
        {
            get
            {
                if ((_AG_IAS_PAYMENT_FILE == null))
                {
                    _AG_IAS_PAYMENT_FILE = new MockObjectSet<AG_IAS_PAYMENT_FILE>("AG_IAS_PAYMENT_FILE");
                }
                return _AG_IAS_PAYMENT_FILE;
            }
        }
        private IObjectSet<AG_IAS_PAYMENT_FILE> _AG_IAS_PAYMENT_FILE;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_SUBPAYMENT_RECEIPT> AG_IAS_SUBPAYMENT_RECEIPT
        {
            get
            {
                if ((_AG_IAS_SUBPAYMENT_RECEIPT == null))
                {
                    _AG_IAS_SUBPAYMENT_RECEIPT = new MockObjectSet<AG_IAS_SUBPAYMENT_RECEIPT>("AG_IAS_SUBPAYMENT_RECEIPT");
                }
                return _AG_IAS_SUBPAYMENT_RECEIPT;
            }
        }
        private IObjectSet<AG_IAS_SUBPAYMENT_RECEIPT> _AG_IAS_SUBPAYMENT_RECEIPT;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_PAYMENT_DETAIL> AG_IAS_PAYMENT_DETAIL
        {
            get
            {
                if ((_AG_IAS_PAYMENT_DETAIL == null))
                {
                    _AG_IAS_PAYMENT_DETAIL = new MockObjectSet<AG_IAS_PAYMENT_DETAIL>("AG_IAS_PAYMENT_DETAIL");
                }
                return _AG_IAS_PAYMENT_DETAIL;
            }
        }
        private IObjectSet<AG_IAS_PAYMENT_DETAIL> _AG_IAS_PAYMENT_DETAIL;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_PAYMENT_HEADER> AG_IAS_PAYMENT_HEADER
        {
            get
            {
                if ((_AG_IAS_PAYMENT_HEADER == null))
                {
                    _AG_IAS_PAYMENT_HEADER = new MockObjectSet<AG_IAS_PAYMENT_HEADER>("AG_IAS_PAYMENT_HEADER");
                }
                return _AG_IAS_PAYMENT_HEADER;
            }
        }
        private IObjectSet<AG_IAS_PAYMENT_HEADER> _AG_IAS_PAYMENT_HEADER;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_PAYMENT_TOTAL> AG_IAS_PAYMENT_TOTAL
        {
            get
            {
                if ((_AG_IAS_PAYMENT_TOTAL == null))
                {
                    _AG_IAS_PAYMENT_TOTAL = new MockObjectSet<AG_IAS_PAYMENT_TOTAL>("AG_IAS_PAYMENT_TOTAL");
                }
                return _AG_IAS_PAYMENT_TOTAL;
            }
        }
        private IObjectSet<AG_IAS_PAYMENT_TOTAL> _AG_IAS_PAYMENT_TOTAL;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_PAYMENT_DETAIL_HIS> AG_IAS_PAYMENT_DETAIL_HIS
        {
            get
            {
                if ((_AG_IAS_PAYMENT_DETAIL_HIS == null))
                {
                    _AG_IAS_PAYMENT_DETAIL_HIS = new MockObjectSet<AG_IAS_PAYMENT_DETAIL_HIS>("AG_IAS_PAYMENT_DETAIL_HIS");
                }
                return _AG_IAS_PAYMENT_DETAIL_HIS;
            }
        }
        private IObjectSet<AG_IAS_PAYMENT_DETAIL_HIS> _AG_IAS_PAYMENT_DETAIL_HIS;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_TRAIN_PERIOD_R> AG_TRAIN_PERIOD_R
        {
            get
            {
                if ((_AG_TRAIN_PERIOD_R == null))
                {
                    _AG_TRAIN_PERIOD_R = new MockObjectSet<AG_TRAIN_PERIOD_R>("AG_TRAIN_PERIOD_R");
                }
                return _AG_TRAIN_PERIOD_R;
            }
        }
        private IObjectSet<AG_TRAIN_PERIOD_R> _AG_TRAIN_PERIOD_R;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_TRAIN_SPECIAL_USED_T> AG_TRAIN_SPECIAL_USED_T
        {
            get
            {
                if ((_AG_TRAIN_SPECIAL_USED_T == null))
                {
                    _AG_TRAIN_SPECIAL_USED_T = new MockObjectSet<AG_TRAIN_SPECIAL_USED_T>("AG_TRAIN_SPECIAL_USED_T");
                }
                return _AG_TRAIN_SPECIAL_USED_T;
            }
        }
        private IObjectSet<AG_TRAIN_SPECIAL_USED_T> _AG_TRAIN_SPECIAL_USED_T;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_TRAIN_SPECIAL_DISCOUNT_T> AG_TRAIN_SPECIAL_DISCOUNT_T
        {
            get
            {
                if ((_AG_TRAIN_SPECIAL_DISCOUNT_T == null))
                {
                    _AG_TRAIN_SPECIAL_DISCOUNT_T = new MockObjectSet<AG_TRAIN_SPECIAL_DISCOUNT_T>("AG_TRAIN_SPECIAL_DISCOUNT_T");
                }
                return _AG_TRAIN_SPECIAL_DISCOUNT_T;
            }
        }
        private IObjectSet<AG_TRAIN_SPECIAL_DISCOUNT_T> _AG_TRAIN_SPECIAL_DISCOUNT_T;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_IAS_SCHEDULE> AG_IAS_SCHEDULE
        {
            get
            {
                if ((_AG_IAS_SCHEDULE == null))
                {
                    _AG_IAS_SCHEDULE = new MockObjectSet<AG_IAS_SCHEDULE>("AG_IAS_SCHEDULE");
                }
                return _AG_IAS_SCHEDULE;
            }
        }
        private IObjectSet<AG_IAS_SCHEDULE> _AG_IAS_SCHEDULE;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public IObjectSet<AG_AGENT_LICENSE_REINSURE_T> AG_AGENT_LICENSE_REINSURE_T
        {
            get
            {
                if ((_AG_AGENT_LICENSE_REINSURE_T == null))
                {
                    _AG_AGENT_LICENSE_REINSURE_T = new MockObjectSet<AG_AGENT_LICENSE_REINSURE_T>("AG_AGENT_LICENSE_REINSURE_T");
                }
                return _AG_AGENT_LICENSE_REINSURE_T;
            }
        }
        private IObjectSet<AG_AGENT_LICENSE_REINSURE_T> _AG_AGENT_LICENSE_REINSURE_T;



        
        #endregion

        //#region AddTo Methods

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_AGENT_LICENSE_JURISTIC_T EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_AGENT_LICENSE_JURISTIC_T(AG_AGENT_LICENSE_JURISTIC_T aG_AGENT_LICENSE_JURISTIC_T)
        //{
        //    base.AddObject("AG_AGENT_LICENSE_JURISTIC_T", aG_AGENT_LICENSE_JURISTIC_T);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_AGENT_LICENSE_PERSON_T EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_AGENT_LICENSE_PERSON_T(AG_AGENT_LICENSE_PERSON_T aG_AGENT_LICENSE_PERSON_T)
        //{
        //    base.AddObject("AG_AGENT_LICENSE_PERSON_T", aG_AGENT_LICENSE_PERSON_T);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_AGENT_LICENSE_T EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_AGENT_LICENSE_T(AG_AGENT_LICENSE_T aG_AGENT_LICENSE_T)
        //{
        //    base.AddObject("AG_AGENT_LICENSE_T", aG_AGENT_LICENSE_T);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_APP_RUNNING_NO_T EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_APP_RUNNING_NO_T(AG_APP_RUNNING_NO_T aG_APP_RUNNING_NO_T)
        //{
        //    base.AddObject("AG_APP_RUNNING_NO_T", aG_APP_RUNNING_NO_T);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_APPLICANT_SCORE_T EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_APPLICANT_SCORE_T(AG_APPLICANT_SCORE_T aG_APPLICANT_SCORE_T)
        //{
        //    base.AddObject("AG_APPLICANT_SCORE_T", aG_APPLICANT_SCORE_T);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_APPLICANT_T EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_APPLICANT_T(AG_APPLICANT_T aG_APPLICANT_T)
        //{
        //    base.AddObject("AG_APPLICANT_T", aG_APPLICANT_T);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_EDUCATION_R EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_EDUCATION_R(AG_EDUCATION_R aG_EDUCATION_R)
        //{
        //    base.AddObject("AG_EDUCATION_R", aG_EDUCATION_R);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_EXAM_LICENSE_R EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_EXAM_LICENSE_R(AG_EXAM_LICENSE_R aG_EXAM_LICENSE_R)
        //{
        //    base.AddObject("AG_EXAM_LICENSE_R", aG_EXAM_LICENSE_R);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_EXAM_PLACE_GROUP_R EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_EXAM_PLACE_GROUP_R(AG_EXAM_PLACE_GROUP_R aG_EXAM_PLACE_GROUP_R)
        //{
        //    base.AddObject("AG_EXAM_PLACE_GROUP_R", aG_EXAM_PLACE_GROUP_R);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_EXAM_PLACE_R EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_EXAM_PLACE_R(AG_EXAM_PLACE_R aG_EXAM_PLACE_R)
        //{
        //    base.AddObject("AG_EXAM_PLACE_R", aG_EXAM_PLACE_R);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_EXAM_TIME_R EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_EXAM_TIME_R(AG_EXAM_TIME_R aG_EXAM_TIME_R)
        //{
        //    base.AddObject("AG_EXAM_TIME_R", aG_EXAM_TIME_R);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_APPLICANT_HEADER_T EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_APPLICANT_HEADER_T(AG_IAS_APPLICANT_HEADER_T aG_IAS_APPLICANT_HEADER_T)
        //{
        //    base.AddObject("AG_IAS_APPLICANT_HEADER_T", aG_IAS_APPLICANT_HEADER_T);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_APPLICANT_HEADER_TEMP EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_APPLICANT_HEADER_TEMP(AG_IAS_APPLICANT_HEADER_TEMP aG_IAS_APPLICANT_HEADER_TEMP)
        //{
        //    base.AddObject("AG_IAS_APPLICANT_HEADER_TEMP", aG_IAS_APPLICANT_HEADER_TEMP);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_APPROVE_CONFIG EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_APPROVE_CONFIG(AG_IAS_APPROVE_CONFIG aG_IAS_APPROVE_CONFIG)
        //{
        //    base.AddObject("AG_IAS_APPROVE_CONFIG", aG_IAS_APPROVE_CONFIG);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_APPROVE_DOC_TYPE EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_APPROVE_DOC_TYPE(AG_IAS_APPROVE_DOC_TYPE aG_IAS_APPROVE_DOC_TYPE)
        //{
        //    base.AddObject("AG_IAS_APPROVE_DOC_TYPE", aG_IAS_APPROVE_DOC_TYPE);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_ATTACH_FILE EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_ATTACH_FILE(AG_IAS_ATTACH_FILE aG_IAS_ATTACH_FILE)
        //{
        //    base.AddObject("AG_IAS_ATTACH_FILE", aG_IAS_ATTACH_FILE);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_FUNCTION_R EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_FUNCTION_R(AG_IAS_FUNCTION_R aG_IAS_FUNCTION_R)
        //{
        //    base.AddObject("AG_IAS_FUNCTION_R", aG_IAS_FUNCTION_R);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_MEMBER_TYPE EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_MEMBER_TYPE(AG_IAS_MEMBER_TYPE aG_IAS_MEMBER_TYPE)
        //{
        //    base.AddObject("AG_IAS_MEMBER_TYPE", aG_IAS_MEMBER_TYPE);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_NATIONALITY EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_NATIONALITY(AG_IAS_NATIONALITY aG_IAS_NATIONALITY)
        //{
        //    base.AddObject("AG_IAS_NATIONALITY", aG_IAS_NATIONALITY);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_OIC_TYPE EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_OIC_TYPE(AG_IAS_OIC_TYPE aG_IAS_OIC_TYPE)
        //{
        //    base.AddObject("AG_IAS_OIC_TYPE", aG_IAS_OIC_TYPE);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_RESET_HISTORY EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_RESET_HISTORY(AG_IAS_RESET_HISTORY aG_IAS_RESET_HISTORY)
        //{
        //    base.AddObject("AG_IAS_RESET_HISTORY", aG_IAS_RESET_HISTORY);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_TEMP_ATTACH_FILE EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_TEMP_ATTACH_FILE(AG_IAS_TEMP_ATTACH_FILE aG_IAS_TEMP_ATTACH_FILE)
        //{
        //    base.AddObject("AG_IAS_TEMP_ATTACH_FILE", aG_IAS_TEMP_ATTACH_FILE);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_LICENSE_RENEW_LOAD EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_LICENSE_RENEW_LOAD(AG_LICENSE_RENEW_LOAD aG_LICENSE_RENEW_LOAD)
        //{
        //    base.AddObject("AG_LICENSE_RENEW_LOAD", aG_LICENSE_RENEW_LOAD);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_LICENSE_RUNNING_NO_T EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_LICENSE_RUNNING_NO_T(AG_LICENSE_RUNNING_NO_T aG_LICENSE_RUNNING_NO_T)
        //{
        //    base.AddObject("AG_LICENSE_RUNNING_NO_T", aG_LICENSE_RUNNING_NO_T);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_LICENSE_T EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_LICENSE_T(AG_LICENSE_T aG_LICENSE_T)
        //{
        //    base.AddObject("AG_LICENSE_T", aG_LICENSE_T);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_PERSONAL_T EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_PERSONAL_T(AG_PERSONAL_T aG_PERSONAL_T)
        //{
        //    base.AddObject("AG_PERSONAL_T", aG_PERSONAL_T);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_TRAIN_PLAN_T EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_TRAIN_PLAN_T(AG_TRAIN_PLAN_T aG_TRAIN_PLAN_T)
        //{
        //    base.AddObject("AG_TRAIN_PLAN_T", aG_TRAIN_PLAN_T);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_TRAIN_SPECIAL_T EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_TRAIN_SPECIAL_T(AG_TRAIN_SPECIAL_T aG_TRAIN_SPECIAL_T)
        //{
        //    base.AddObject("AG_TRAIN_SPECIAL_T", aG_TRAIN_SPECIAL_T);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the VW_IAS_AMPUR EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToVW_IAS_AMPUR(VW_IAS_AMPUR vW_IAS_AMPUR)
        //{
        //    base.AddObject("VW_IAS_AMPUR", vW_IAS_AMPUR);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the VW_IAS_COM_CODE EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToVW_IAS_COM_CODE(VW_IAS_COM_CODE vW_IAS_COM_CODE)
        //{
        //    base.AddObject("VW_IAS_COM_CODE", vW_IAS_COM_CODE);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the VW_IAS_PROVINCE EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToVW_IAS_PROVINCE(VW_IAS_PROVINCE vW_IAS_PROVINCE)
        //{
        //    base.AddObject("VW_IAS_PROVINCE", vW_IAS_PROVINCE);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the VW_IAS_TITLE_NAME EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToVW_IAS_TITLE_NAME(VW_IAS_TITLE_NAME vW_IAS_TITLE_NAME)
        //{
        //    base.AddObject("VW_IAS_TITLE_NAME", vW_IAS_TITLE_NAME);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the VW_IAS_TUMBON EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToVW_IAS_TUMBON(VW_IAS_TUMBON vW_IAS_TUMBON)
        //{
        //    base.AddObject("VW_IAS_TUMBON", vW_IAS_TUMBON);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_STATUS EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_STATUS(AG_IAS_STATUS aG_IAS_STATUS)
        //{
        //    base.AddObject("AG_IAS_STATUS", aG_IAS_STATUS);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_LICENSE_RENEW_T EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_LICENSE_RENEW_T(AG_LICENSE_RENEW_T aG_LICENSE_RENEW_T)
        //{
        //    base.AddObject("AG_LICENSE_RENEW_T", aG_LICENSE_RENEW_T);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_LICENSE_H_TEMP EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_LICENSE_H_TEMP(AG_IAS_LICENSE_H_TEMP aG_IAS_LICENSE_H_TEMP)
        //{
        //    base.AddObject("AG_IAS_LICENSE_H_TEMP", aG_IAS_LICENSE_H_TEMP);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_APPLICANT_SCORE_H_TEMP EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_APPLICANT_SCORE_H_TEMP(AG_IAS_APPLICANT_SCORE_H_TEMP aG_IAS_APPLICANT_SCORE_H_TEMP)
        //{
        //    base.AddObject("AG_IAS_APPLICANT_SCORE_H_TEMP", aG_IAS_APPLICANT_SCORE_H_TEMP);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_INVOICE_T EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_INVOICE_T(AG_IAS_INVOICE_T aG_IAS_INVOICE_T)
        //{
        //    base.AddObject("AG_IAS_INVOICE_T", aG_IAS_INVOICE_T);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_PAYMENT_HEADER_T EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_PAYMENT_HEADER_T(AG_IAS_PAYMENT_HEADER_T aG_IAS_PAYMENT_HEADER_T)
        //{
        //    base.AddObject("AG_IAS_PAYMENT_HEADER_T", aG_IAS_PAYMENT_HEADER_T);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_USERS_RIGHT EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_USERS_RIGHT(AG_IAS_USERS_RIGHT aG_IAS_USERS_RIGHT)
        //{
        //    base.AddObject("AG_IAS_USERS_RIGHT", aG_IAS_USERS_RIGHT);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_TEMP_PAYMENT_HEADER EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_TEMP_PAYMENT_HEADER(AG_IAS_TEMP_PAYMENT_HEADER aG_IAS_TEMP_PAYMENT_HEADER)
        //{
        //    base.AddObject("AG_IAS_TEMP_PAYMENT_HEADER", aG_IAS_TEMP_PAYMENT_HEADER);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_TEMP_PAYMENT_DETAIL EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_TEMP_PAYMENT_DETAIL(AG_IAS_TEMP_PAYMENT_DETAIL aG_IAS_TEMP_PAYMENT_DETAIL)
        //{
        //    base.AddObject("AG_IAS_TEMP_PAYMENT_DETAIL", aG_IAS_TEMP_PAYMENT_DETAIL);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_TEMP_PAYMENT_TOTAL EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_TEMP_PAYMENT_TOTAL(AG_IAS_TEMP_PAYMENT_TOTAL aG_IAS_TEMP_PAYMENT_TOTAL)
        //{
        //    base.AddObject("AG_IAS_TEMP_PAYMENT_TOTAL", aG_IAS_TEMP_PAYMENT_TOTAL);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_BILL_CODE EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_BILL_CODE(AG_IAS_BILL_CODE aG_IAS_BILL_CODE)
        //{
        //    base.AddObject("AG_IAS_BILL_CODE", aG_IAS_BILL_CODE);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_LICENSE_D_TEMP EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_LICENSE_D_TEMP(AG_IAS_LICENSE_D_TEMP aG_IAS_LICENSE_D_TEMP)
        //{
        //    base.AddObject("AG_IAS_LICENSE_D_TEMP", aG_IAS_LICENSE_D_TEMP);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_LOG_ACTIVITY EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_LOG_ACTIVITY(AG_IAS_LOG_ACTIVITY aG_IAS_LOG_ACTIVITY)
        //{
        //    base.AddObject("AG_IAS_LOG_ACTIVITY", aG_IAS_LOG_ACTIVITY);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_ACCEPT_OFF_R EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_ACCEPT_OFF_R(AG_ACCEPT_OFF_R aG_ACCEPT_OFF_R)
        //{
        //    base.AddObject("AG_ACCEPT_OFF_R", aG_ACCEPT_OFF_R);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_LICENSE_TYPE_R EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_LICENSE_TYPE_R(AG_LICENSE_TYPE_R aG_LICENSE_TYPE_R)
        //{
        //    base.AddObject("AG_LICENSE_TYPE_R", aG_LICENSE_TYPE_R);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_PETITION_TYPE_R EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_PETITION_TYPE_R(AG_PETITION_TYPE_R aG_PETITION_TYPE_R)
        //{
        //    base.AddObject("AG_PETITION_TYPE_R", aG_PETITION_TYPE_R);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_APPLICANT_DETAIL_TEMP EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_APPLICANT_DETAIL_TEMP(AG_IAS_APPLICANT_DETAIL_TEMP aG_IAS_APPLICANT_DETAIL_TEMP)
        //{
        //    base.AddObject("AG_IAS_APPLICANT_DETAIL_TEMP", aG_IAS_APPLICANT_DETAIL_TEMP);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_ATTACH_FILE_LICENSE2 EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_ATTACH_FILE_LICENSE2(AG_IAS_ATTACH_FILE_LICENSE2 aG_IAS_ATTACH_FILE_LICENSE2)
        //{
        //    base.AddObject("AG_IAS_ATTACH_FILE_LICENSE2", aG_IAS_ATTACH_FILE_LICENSE2);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_IMPORT_HEADER_TEMP EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_IMPORT_HEADER_TEMP(AG_IAS_IMPORT_HEADER_TEMP aG_IAS_IMPORT_HEADER_TEMP)
        //{
        //    base.AddObject("AG_IAS_IMPORT_HEADER_TEMP", aG_IAS_IMPORT_HEADER_TEMP);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_IMPORT_DETAIL_TEMP EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_IMPORT_DETAIL_TEMP(AG_IAS_IMPORT_DETAIL_TEMP aG_IAS_IMPORT_DETAIL_TEMP)
        //{
        //    base.AddObject("AG_IAS_IMPORT_DETAIL_TEMP", aG_IAS_IMPORT_DETAIL_TEMP);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_ATTACH_FILE_LICENSE EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_ATTACH_FILE_LICENSE(AG_IAS_ATTACH_FILE_LICENSE aG_IAS_ATTACH_FILE_LICENSE)
        //{
        //    base.AddObject("AG_IAS_ATTACH_FILE_LICENSE", aG_IAS_ATTACH_FILE_LICENSE);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_DOCUMENT_TYPE EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_DOCUMENT_TYPE(AG_IAS_DOCUMENT_TYPE aG_IAS_DOCUMENT_TYPE)
        //{
        //    base.AddObject("AG_IAS_DOCUMENT_TYPE", aG_IAS_DOCUMENT_TYPE);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_PETITION_TYPE_R EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_PETITION_TYPE_R(AG_IAS_PETITION_TYPE_R aG_IAS_PETITION_TYPE_R)
        //{
        //    base.AddObject("AG_IAS_PETITION_TYPE_R", aG_IAS_PETITION_TYPE_R);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_PAYMENT_G_T EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_PAYMENT_G_T(AG_IAS_PAYMENT_G_T aG_IAS_PAYMENT_G_T)
        //{
        //    base.AddObject("AG_IAS_PAYMENT_G_T", aG_IAS_PAYMENT_G_T);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_PAYMENT_RUNNINGNO EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_PAYMENT_RUNNINGNO(AG_IAS_PAYMENT_RUNNINGNO aG_IAS_PAYMENT_RUNNINGNO)
        //{
        //    base.AddObject("AG_IAS_PAYMENT_RUNNINGNO", aG_IAS_PAYMENT_RUNNINGNO);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_LICENSE_TYPE_R EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_LICENSE_TYPE_R(AG_IAS_LICENSE_TYPE_R aG_IAS_LICENSE_TYPE_R)
        //{
        //    base.AddObject("AG_IAS_LICENSE_TYPE_R", aG_IAS_LICENSE_TYPE_R);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the VW_AS_COMPANY_T_BC1 EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToVW_AS_COMPANY_T_BC1(VW_AS_COMPANY_T_BC1 vW_AS_COMPANY_T_BC1)
        //{
        //    base.AddObject("VW_AS_COMPANY_T_BC1", vW_AS_COMPANY_T_BC1);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the VW_AS_COMPANY_T_BC2 EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToVW_AS_COMPANY_T_BC2(VW_AS_COMPANY_T_BC2 vW_AS_COMPANY_T_BC2)
        //{
        //    base.AddObject("VW_AS_COMPANY_T_BC2", vW_AS_COMPANY_T_BC2);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the VW_AS_BUSI_TYPE_R EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToVW_AS_BUSI_TYPE_R(VW_AS_BUSI_TYPE_R vW_AS_BUSI_TYPE_R)
        //{
        //    base.AddObject("VW_AS_BUSI_TYPE_R", vW_AS_BUSI_TYPE_R);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the VW_AS_COMPANY_T EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToVW_AS_COMPANY_T(VW_AS_COMPANY_T vW_AS_COMPANY_T)
        //{
        //    base.AddObject("VW_AS_COMPANY_T", vW_AS_COMPANY_T);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_TRAIN_T EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_TRAIN_T(AG_TRAIN_T aG_TRAIN_T)
        //{
        //    base.AddObject("AG_TRAIN_T", aG_TRAIN_T);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_U_TRAIN_T EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_U_TRAIN_T(AG_U_TRAIN_T aG_U_TRAIN_T)
        //{
        //    base.AddObject("AG_U_TRAIN_T", aG_U_TRAIN_T);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_PERSON_INCOMP_AGENT_T EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_PERSON_INCOMP_AGENT_T(AG_PERSON_INCOMP_AGENT_T aG_PERSON_INCOMP_AGENT_T)
        //{
        //    base.AddObject("AG_PERSON_INCOMP_AGENT_T", aG_PERSON_INCOMP_AGENT_T);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_LICENSE_H EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_LICENSE_H(AG_IAS_LICENSE_H aG_IAS_LICENSE_H)
        //{
        //    base.AddObject("AG_IAS_LICENSE_H", aG_IAS_LICENSE_H);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_SUBPAYMENT_H_T EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_SUBPAYMENT_H_T(AG_IAS_SUBPAYMENT_H_T aG_IAS_SUBPAYMENT_H_T)
        //{
        //    base.AddObject("AG_IAS_SUBPAYMENT_H_T", aG_IAS_SUBPAYMENT_H_T);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_LICENSE_D EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_LICENSE_D(AG_IAS_LICENSE_D aG_IAS_LICENSE_D)
        //{
        //    base.AddObject("AG_IAS_LICENSE_D", aG_IAS_LICENSE_D);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_DOCUMENT_TYPE_T EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_DOCUMENT_TYPE_T(AG_IAS_DOCUMENT_TYPE_T aG_IAS_DOCUMENT_TYPE_T)
        //{
        //    base.AddObject("AG_IAS_DOCUMENT_TYPE_T", aG_IAS_DOCUMENT_TYPE_T);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_APPLICANT_SCORE_D_TEMP EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_APPLICANT_SCORE_D_TEMP(AG_IAS_APPLICANT_SCORE_D_TEMP aG_IAS_APPLICANT_SCORE_D_TEMP)
        //{
        //    base.AddObject("AG_IAS_APPLICANT_SCORE_D_TEMP", aG_IAS_APPLICANT_SCORE_D_TEMP);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_AGENT_T EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_AGENT_T(AG_AGENT_T aG_AGENT_T)
        //{
        //    base.AddObject("AG_AGENT_T", aG_AGENT_T);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_HIS_MOVE_COMP_AGENT_T EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_HIS_MOVE_COMP_AGENT_T(AG_HIS_MOVE_COMP_AGENT_T aG_HIS_MOVE_COMP_AGENT_T)
        //{
        //    base.AddObject("AG_HIS_MOVE_COMP_AGENT_T", aG_HIS_MOVE_COMP_AGENT_T);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_REGISTRATION_T EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_REGISTRATION_T(AG_IAS_REGISTRATION_T aG_IAS_REGISTRATION_T)
        //{
        //    base.AddObject("AG_IAS_REGISTRATION_T", aG_IAS_REGISTRATION_T);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_AGENT_TYPE_R EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_AGENT_TYPE_R(AG_AGENT_TYPE_R aG_AGENT_TYPE_R)
        //{
        //    base.AddObject("AG_AGENT_TYPE_R", aG_AGENT_TYPE_R);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_PAYMENT_EXPIRE_DAY EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_PAYMENT_EXPIRE_DAY(AG_IAS_PAYMENT_EXPIRE_DAY aG_IAS_PAYMENT_EXPIRE_DAY)
        //{
        //    base.AddObject("AG_IAS_PAYMENT_EXPIRE_DAY", aG_IAS_PAYMENT_EXPIRE_DAY);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_RECEIPT_HISTORY EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_RECEIPT_HISTORY(AG_IAS_RECEIPT_HISTORY aG_IAS_RECEIPT_HISTORY)
        //{
        //    base.AddObject("AG_IAS_RECEIPT_HISTORY", aG_IAS_RECEIPT_HISTORY);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the VW_IAS_TITLE_NAME_PRIORITY EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToVW_IAS_TITLE_NAME_PRIORITY(VW_IAS_TITLE_NAME_PRIORITY vW_IAS_TITLE_NAME_PRIORITY)
        //{
        //    base.AddObject("VW_IAS_TITLE_NAME_PRIORITY", vW_IAS_TITLE_NAME_PRIORITY);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_USERS EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_USERS(AG_IAS_USERS aG_IAS_USERS)
        //{
        //    base.AddObject("AG_IAS_USERS", aG_IAS_USERS);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_PAYMENT_T EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_PAYMENT_T(AG_IAS_PAYMENT_T aG_IAS_PAYMENT_T)
        //{
        //    base.AddObject("AG_IAS_PAYMENT_T", aG_IAS_PAYMENT_T);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_PERSONAL_T EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_PERSONAL_T(AG_IAS_PERSONAL_T aG_IAS_PERSONAL_T)
        //{
        //    base.AddObject("AG_IAS_PERSONAL_T", aG_IAS_PERSONAL_T);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_TRAIN_PERSON_T EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_TRAIN_PERSON_T(AG_TRAIN_PERSON_T aG_TRAIN_PERSON_T)
        //{
        //    base.AddObject("AG_TRAIN_PERSON_T", aG_TRAIN_PERSON_T);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_TRAIN_SPECIAL_R EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_TRAIN_SPECIAL_R(AG_TRAIN_SPECIAL_R aG_TRAIN_SPECIAL_R)
        //{
        //    base.AddObject("AG_TRAIN_SPECIAL_R", aG_TRAIN_SPECIAL_R);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_SUBPAYMENT_D_T EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_SUBPAYMENT_D_T(AG_IAS_SUBPAYMENT_D_T aG_IAS_SUBPAYMENT_D_T)
        //{
        //    base.AddObject("AG_IAS_SUBPAYMENT_D_T", aG_IAS_SUBPAYMENT_D_T);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_SUBJECT_R EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_SUBJECT_R(AG_SUBJECT_R aG_SUBJECT_R)
        //{
        //    base.AddObject("AG_SUBJECT_R", aG_SUBJECT_R);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_ASSOCIATION EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_ASSOCIATION(AG_IAS_ASSOCIATION aG_IAS_ASSOCIATION)
        //{
        //    base.AddObject("AG_IAS_ASSOCIATION", aG_IAS_ASSOCIATION);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_EXAM_ROOM EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_EXAM_ROOM(AG_IAS_EXAM_ROOM aG_IAS_EXAM_ROOM)
        //{
        //    base.AddObject("AG_IAS_EXAM_ROOM", aG_IAS_EXAM_ROOM);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_HIST_PERSONAL_T EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_HIST_PERSONAL_T(AG_IAS_HIST_PERSONAL_T aG_IAS_HIST_PERSONAL_T)
        //{
        //    base.AddObject("AG_IAS_HIST_PERSONAL_T", aG_IAS_HIST_PERSONAL_T);
        //}

        ///// <summary>
        ///// Deprecated Method for adding a new object to the AG_IAS_TEMP_PERSONAL_T EntitySet. Consider using the .Add method of the associated IObjectSet&lt;T&gt; property instead.
        ///// </summary>
        //public void AddToAG_IAS_TEMP_PERSONAL_T(AG_IAS_TEMP_PERSONAL_T aG_IAS_TEMP_PERSONAL_T)
        //{
        //    base.AddObject("AG_IAS_TEMP_PERSONAL_T", aG_IAS_TEMP_PERSONAL_T);
        //}

        //#endregion


        #region AddTo Methods

        public void AddToAG_AGENT_LICENSE_JURISTIC_T(AG_AGENT_LICENSE_JURISTIC_T aG_AGENT_LICENSE_JURISTIC_T)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_AGENT_LICENSE_PERSON_T(AG_AGENT_LICENSE_PERSON_T aG_AGENT_LICENSE_PERSON_T)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_AGENT_LICENSE_T(AG_AGENT_LICENSE_T aG_AGENT_LICENSE_T)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_APP_RUNNING_NO_T(AG_APP_RUNNING_NO_T aG_APP_RUNNING_NO_T)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_APPLICANT_SCORE_T(AG_APPLICANT_SCORE_T aG_APPLICANT_SCORE_T)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_APPLICANT_T(AG_APPLICANT_T aG_APPLICANT_T)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_EDUCATION_R(AG_EDUCATION_R aG_EDUCATION_R)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_EXAM_LICENSE_R(AG_EXAM_LICENSE_R aG_EXAM_LICENSE_R)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_EXAM_PLACE_GROUP_R(AG_EXAM_PLACE_GROUP_R aG_EXAM_PLACE_GROUP_R)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_EXAM_PLACE_R(AG_EXAM_PLACE_R aG_EXAM_PLACE_R)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_EXAM_TIME_R(AG_EXAM_TIME_R aG_EXAM_TIME_R)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_APPLICANT_HEADER_T(AG_IAS_APPLICANT_HEADER_T aG_IAS_APPLICANT_HEADER_T)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_APPLICANT_HEADER_TEMP(AG_IAS_APPLICANT_HEADER_TEMP aG_IAS_APPLICANT_HEADER_TEMP)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_APPROVE_CONFIG(AG_IAS_APPROVE_CONFIG aG_IAS_APPROVE_CONFIG)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_APPROVE_DOC_TYPE(AG_IAS_APPROVE_DOC_TYPE aG_IAS_APPROVE_DOC_TYPE)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_ATTACH_FILE(AG_IAS_ATTACH_FILE aG_IAS_ATTACH_FILE)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_FUNCTION_R(AG_IAS_FUNCTION_R aG_IAS_FUNCTION_R)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_MEMBER_TYPE(AG_IAS_MEMBER_TYPE aG_IAS_MEMBER_TYPE)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_NATIONALITY(AG_IAS_NATIONALITY aG_IAS_NATIONALITY)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_OIC_TYPE(AG_IAS_OIC_TYPE aG_IAS_OIC_TYPE)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_RESET_HISTORY(AG_IAS_RESET_HISTORY aG_IAS_RESET_HISTORY)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_TEMP_ATTACH_FILE(AG_IAS_TEMP_ATTACH_FILE aG_IAS_TEMP_ATTACH_FILE)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_LICENSE_RENEW_LOAD(AG_LICENSE_RENEW_LOAD aG_LICENSE_RENEW_LOAD)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_LICENSE_RUNNING_NO_T(AG_LICENSE_RUNNING_NO_T aG_LICENSE_RUNNING_NO_T)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_LICENSE_T(AG_LICENSE_T aG_LICENSE_T)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_PERSONAL_T(AG_PERSONAL_T aG_PERSONAL_T)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_TRAIN_PLAN_T(AG_TRAIN_PLAN_T aG_TRAIN_PLAN_T)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_TRAIN_SPECIAL_T(AG_TRAIN_SPECIAL_T aG_TRAIN_SPECIAL_T)
        {
            throw new NotImplementedException();
        }

        public void AddToVW_IAS_AMPUR(VW_IAS_AMPUR vW_IAS_AMPUR)
        {
            throw new NotImplementedException();
        }

        public void AddToVW_IAS_COM_CODE(VW_IAS_COM_CODE vW_IAS_COM_CODE)
        {
            throw new NotImplementedException();
        }

        public void AddToVW_IAS_PROVINCE(VW_IAS_PROVINCE vW_IAS_PROVINCE)
        {
            throw new NotImplementedException();
        }

        public void AddToVW_IAS_TITLE_NAME(VW_IAS_TITLE_NAME vW_IAS_TITLE_NAME)
        {
            throw new NotImplementedException();
        }

        public void AddToVW_IAS_TUMBON(VW_IAS_TUMBON vW_IAS_TUMBON)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_STATUS(AG_IAS_STATUS aG_IAS_STATUS)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_LICENSE_RENEW_T(AG_LICENSE_RENEW_T aG_LICENSE_RENEW_T)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_LICENSE_H_TEMP(AG_IAS_LICENSE_H_TEMP aG_IAS_LICENSE_H_TEMP)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_APPLICANT_SCORE_H_TEMP(AG_IAS_APPLICANT_SCORE_H_TEMP aG_IAS_APPLICANT_SCORE_H_TEMP)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_INVOICE_T(AG_IAS_INVOICE_T aG_IAS_INVOICE_T)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_PAYMENT_HEADER_T(AG_IAS_PAYMENT_HEADER_T aG_IAS_PAYMENT_HEADER_T)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_USERS_RIGHT(AG_IAS_USERS_RIGHT aG_IAS_USERS_RIGHT)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_TEMP_PAYMENT_HEADER(AG_IAS_TEMP_PAYMENT_HEADER aG_IAS_TEMP_PAYMENT_HEADER)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_TEMP_PAYMENT_DETAIL(AG_IAS_TEMP_PAYMENT_DETAIL aG_IAS_TEMP_PAYMENT_DETAIL)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_TEMP_PAYMENT_TOTAL(AG_IAS_TEMP_PAYMENT_TOTAL aG_IAS_TEMP_PAYMENT_TOTAL)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_BILL_CODE(AG_IAS_BILL_CODE aG_IAS_BILL_CODE)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_LICENSE_D_TEMP(AG_IAS_LICENSE_D_TEMP aG_IAS_LICENSE_D_TEMP)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_LOG_ACTIVITY(AG_IAS_LOG_ACTIVITY aG_IAS_LOG_ACTIVITY)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_ACCEPT_OFF_R(AG_ACCEPT_OFF_R aG_ACCEPT_OFF_R)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_LICENSE_TYPE_R(AG_LICENSE_TYPE_R aG_LICENSE_TYPE_R)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_PETITION_TYPE_R(AG_PETITION_TYPE_R aG_PETITION_TYPE_R)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_APPLICANT_DETAIL_TEMP(AG_IAS_APPLICANT_DETAIL_TEMP aG_IAS_APPLICANT_DETAIL_TEMP)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_ATTACH_FILE_LICENSE2(AG_IAS_ATTACH_FILE_LICENSE2 aG_IAS_ATTACH_FILE_LICENSE2)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_IMPORT_HEADER_TEMP(AG_IAS_IMPORT_HEADER_TEMP aG_IAS_IMPORT_HEADER_TEMP)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_IMPORT_DETAIL_TEMP(AG_IAS_IMPORT_DETAIL_TEMP aG_IAS_IMPORT_DETAIL_TEMP)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_ATTACH_FILE_LICENSE(AG_IAS_ATTACH_FILE_LICENSE aG_IAS_ATTACH_FILE_LICENSE)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_DOCUMENT_TYPE(AG_IAS_DOCUMENT_TYPE aG_IAS_DOCUMENT_TYPE)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_PETITION_TYPE_R(AG_IAS_PETITION_TYPE_R aG_IAS_PETITION_TYPE_R)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_PAYMENT_G_T(AG_IAS_PAYMENT_G_T aG_IAS_PAYMENT_G_T)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_PAYMENT_RUNNINGNO(AG_IAS_PAYMENT_RUNNINGNO aG_IAS_PAYMENT_RUNNINGNO)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_LICENSE_TYPE_R(AG_IAS_LICENSE_TYPE_R aG_IAS_LICENSE_TYPE_R)
        {
            throw new NotImplementedException();
        }

        public void AddToVW_AS_COMPANY_T_BC1(VW_AS_COMPANY_T_BC1 vW_AS_COMPANY_T_BC1)
        {
            throw new NotImplementedException();
        }

        public void AddToVW_AS_COMPANY_T_BC2(VW_AS_COMPANY_T_BC2 vW_AS_COMPANY_T_BC2)
        {
            throw new NotImplementedException();
        }

        public void AddToVW_AS_BUSI_TYPE_R(VW_AS_BUSI_TYPE_R vW_AS_BUSI_TYPE_R)
        {
            throw new NotImplementedException();
        }

        public void AddToVW_AS_COMPANY_T(VW_AS_COMPANY_T vW_AS_COMPANY_T)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_TRAIN_T(AG_TRAIN_T aG_TRAIN_T)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_U_TRAIN_T(AG_U_TRAIN_T aG_U_TRAIN_T)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_PERSON_INCOMP_AGENT_T(AG_PERSON_INCOMP_AGENT_T aG_PERSON_INCOMP_AGENT_T)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_LICENSE_H(AG_IAS_LICENSE_H aG_IAS_LICENSE_H)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_SUBPAYMENT_H_T(AG_IAS_SUBPAYMENT_H_T aG_IAS_SUBPAYMENT_H_T)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_LICENSE_D(AG_IAS_LICENSE_D aG_IAS_LICENSE_D)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_DOCUMENT_TYPE_T(AG_IAS_DOCUMENT_TYPE_T aG_IAS_DOCUMENT_TYPE_T)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_APPLICANT_SCORE_D_TEMP(AG_IAS_APPLICANT_SCORE_D_TEMP aG_IAS_APPLICANT_SCORE_D_TEMP)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_AGENT_T(AG_AGENT_T aG_AGENT_T)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_HIS_MOVE_COMP_AGENT_T(AG_HIS_MOVE_COMP_AGENT_T aG_HIS_MOVE_COMP_AGENT_T)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_REGISTRATION_T(AG_IAS_REGISTRATION_T aG_IAS_REGISTRATION_T)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_AGENT_TYPE_R(AG_AGENT_TYPE_R aG_AGENT_TYPE_R)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_PAYMENT_EXPIRE_DAY(AG_IAS_PAYMENT_EXPIRE_DAY aG_IAS_PAYMENT_EXPIRE_DAY)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_RECEIPT_HISTORY(AG_IAS_RECEIPT_HISTORY aG_IAS_RECEIPT_HISTORY)
        {
            throw new NotImplementedException();
        }

        public void AddToVW_IAS_TITLE_NAME_PRIORITY(VW_IAS_TITLE_NAME_PRIORITY vW_IAS_TITLE_NAME_PRIORITY)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_USERS(AG_IAS_USERS aG_IAS_USERS)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_PAYMENT_T(AG_IAS_PAYMENT_T aG_IAS_PAYMENT_T)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_PERSONAL_T(AG_IAS_PERSONAL_T aG_IAS_PERSONAL_T)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_TRAIN_PERSON_T(AG_TRAIN_PERSON_T aG_TRAIN_PERSON_T)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_TRAIN_SPECIAL_R(AG_TRAIN_SPECIAL_R aG_TRAIN_SPECIAL_R)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_SUBPAYMENT_D_T(AG_IAS_SUBPAYMENT_D_T aG_IAS_SUBPAYMENT_D_T)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_SUBJECT_R(AG_SUBJECT_R aG_SUBJECT_R)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_ASSOCIATION(AG_IAS_ASSOCIATION aG_IAS_ASSOCIATION)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_EXAM_PLACE_ROOM(AG_IAS_EXAM_PLACE_ROOM aG_AG_IAS_EXAM_PLACE_ROOM)
        {
            throw new NotImplementedException();       
        }

        public void AddToAG_IAS_HIST_PERSONAL_T(AG_IAS_HIST_PERSONAL_T aG_IAS_HIST_PERSONAL_T)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_TEMP_PERSONAL_T(AG_IAS_TEMP_PERSONAL_T aG_IAS_TEMP_PERSONAL_T)
        {
            throw new NotImplementedException();
        }

        public int? CommandTimeout
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public System.Data.Common.DbConnection Connection
        {
            get { throw new NotImplementedException(); }
        }

        public ObjectContextOptions ContextOptions
        {
            get { throw new NotImplementedException(); }
        }

        public string DefaultContainerName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public System.Data.Metadata.Edm.MetadataWorkspace MetadataWorkspace
        {
            get { throw new NotImplementedException(); }
        }

        public ObjectStateManager ObjectStateManager
        {
            get { throw new NotImplementedException(); }
        }

        public event ObjectMaterializedEventHandler ObjectMaterialized;

        public event EventHandler SavingChanges;

        public void AcceptAllChanges()
        {
            throw new NotImplementedException();
        }

        public void AddObject(string entitySetName, object entity)
        {
            throw new NotImplementedException();
        }

        public TEntity ApplyCurrentValues<TEntity>(string entitySetName, TEntity currentEntity) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public TEntity ApplyOriginalValues<TEntity>(string entitySetName, TEntity originalEntity) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void ApplyPropertyChanges(string entitySetName, object changed)
        {
            throw new NotImplementedException();
        }

        public void Attach(System.Data.Objects.DataClasses.IEntityWithKey entity)
        {
            throw new NotImplementedException();
        }

        public void AttachTo(string entitySetName, object entity)
        {
            throw new NotImplementedException();
        }

        public void CreateDatabase()
        {
            throw new NotImplementedException();
        }

        public string CreateDatabaseScript()
        {
            throw new NotImplementedException();
        }

        public System.Data.EntityKey CreateEntityKey(string entitySetName, object entity)
        {
            throw new NotImplementedException();
        }

        public T CreateObject<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public ObjectSet<TEntity> CreateObjectSet<TEntity>() where TEntity : class
        {
            throw new NotImplementedException();
        }

        public ObjectSet<TEntity> CreateObjectSet<TEntity>(string entitySetName) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void CreateProxyTypes(IEnumerable<Type> types)
        {
            throw new NotImplementedException();
        }

        public ObjectQuery<T> CreateQuery<T>(string queryString, params ObjectParameter[] parameters)
        {
            throw new NotImplementedException();
        }

        public bool DatabaseExists()
        {
            throw new NotImplementedException();
        }

        public void DeleteDatabase()
        {
            throw new NotImplementedException();
        }

        public void DeleteObject(object entity)
        {
            throw new NotImplementedException();
        }

        public void Detach(object entity)
        {
            throw new NotImplementedException();
        }

        public void DetectChanges()
        {
        
        }

        public void Dispose()
        {
       
        }

        public int ExecuteFunction(string functionName, params ObjectParameter[] parameters)
        {
            throw new NotImplementedException();
        }

        public ObjectResult<TElement> ExecuteFunction<TElement>(string functionName, params ObjectParameter[] parameters)
        {
            throw new NotImplementedException();
        }

        public ObjectResult<TElement> ExecuteFunction<TElement>(string functionName, MergeOption mergeOption, params ObjectParameter[] parameters)
        {
            throw new NotImplementedException();
        }

        public int ExecuteStoreCommand(string commandText, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public ObjectResult<TElement> ExecuteStoreQuery<TElement>(string commandText, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public ObjectResult<TEntity> ExecuteStoreQuery<TEntity>(string commandText, string entitySetName, MergeOption mergeOption, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public object GetObjectByKey(System.Data.EntityKey key)
        {
            throw new NotImplementedException();
        }

        public void LoadProperty(object entity, string navigationProperty)
        {
            throw new NotImplementedException();
        }

        public void LoadProperty<TEntity>(TEntity entity, System.Linq.Expressions.Expression<Func<TEntity, object>> selector)
        {
            throw new NotImplementedException();
        }

        public void LoadProperty(object entity, string navigationProperty, MergeOption mergeOption)
        {
            throw new NotImplementedException();
        }

        public void LoadProperty<TEntity>(TEntity entity, System.Linq.Expressions.Expression<Func<TEntity, object>> selector, MergeOption mergeOption)
        {
            throw new NotImplementedException();
        }

        public void Refresh(RefreshMode refreshMode, System.Collections.IEnumerable collection)
        {
            throw new NotImplementedException();
        }

        public void Refresh(RefreshMode refreshMode, object entity)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            return 0;
        }

        public int SaveChanges(bool acceptChangesDuringSave)
        {
            return 0;
        }

        public int SaveChanges(SaveOptions options)
        {
            return 0;
        }

        public ObjectResult<TElement> Translate<TElement>(System.Data.Common.DbDataReader reader)
        {
            throw new NotImplementedException();
        }

        public ObjectResult<TEntity> Translate<TEntity>(System.Data.Common.DbDataReader reader, string entitySetName, MergeOption mergeOption)
        {
            throw new NotImplementedException();
        }

        public bool TryGetObjectByKey(System.Data.EntityKey key, out object value)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_EXAM_ROOM_LICENSE_R(AG_IAS_EXAM_ROOM_LICENSE_R aG_IAS_EXAM_SUBLICENSE_R)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_CONFIG(AG_IAS_CONFIG aG_IAS_CONFIG)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_ASSOCIATION_LICENSE(AG_IAS_ASSOCIATION_LICENSE aAG_IAS_ASSOCIATION_LICENSE)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_EXAM_CONDITION_R(AG_EXAM_CONDITION_R aG_EXAM_CONDITION_R)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_ATTACH_FILE_APPLICANT(AG_IAS_ATTACH_FILE_APPLICANT aG_IAS_ATTACH_FILE_APPLICANT)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_APPLICANT_CHANGE(AG_IAS_APPLICANT_CHANGE aG_IAS_APPLICANT_CHANGE)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_APPLICANT_T_LOG(AG_IAS_APPLICANT_T_LOG aG_IAS_APPLICANT_T_LOG)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_VALIDATE_LICENSE(AG_IAS_VALIDATE_LICENSE aG_IAS_VALIDATE_LICENSE)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_VALIDATE_LICENSE_CON(AG_IAS_VALIDATE_LICENSE_CON aG_IAS_VALIDATE_LICENSE_CON)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_VALIDATE_LICENSE_GROUP(AG_IAS_VALIDATE_LICENSE_GROUP aG_IAS_VALIDATE_LICENSE_GROUP)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_SUBJECT_GROUP(AG_IAS_SUBJECT_GROUP aG_IAS_SUBJECT_GROUP)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_EXAM_CONDITION_GROUP(AG_IAS_EXAM_CONDITION_GROUP aG_IAS_EXAM_CONDITION_GROUP)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_EXAM_CONDITION_GROUP_D(AG_IAS_EXAM_CONDITION_GROUP_D aG_IAS_EXAM_CONDITION_GROUP_D)
        {
            throw new NotImplementedException();
        }


        public void AddToAG_IAS_APPROVE_FIELD(AG_IAS_APPROVE_FIELD aG_IAS_APPROVE_FIELD)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_ASSOCIATION_APPROVE(AG_IAS_ASSOCIATION_APPROVE aG_IAS_ASSOCIATION_APPROVE)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_EXAM_SPECIAL_R(AG_IAS_EXAM_SPECIAL_R aG_IAS_EXAM_SPECIAL_R)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_EXAM_SPECIAL_T(AG_IAS_EXAM_SPECIAL_T aG_IAS_EXAM_SPECIAL_T)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_SPECIAL_T_TEMP(AG_IAS_SPECIAL_T_TEMP aG_IAS_SPECIAL_T_TEMP)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_EXAM_SUBJECT_GROUP(AG_IAS_EXAM_SUBJECT_GROUP aG_IAS_EXAM_SUBJECT_GROUP)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_TRAIN_HOUR_T(AG_IAS_TRAIN_HOUR_T aG_IAS_TRAIN_HOUR_T)
        {
            throw new NotImplementedException();
        }

        public void AddToAG_IAS_APPLICANT_ROOM(AG_IAS_APPLICANT_ROOM aG_IAS_APPLICANT_ROOM)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deprecated Method for adding a new object to the AG_IAS_TEMP_PAYMENT_DETAIL_HIS EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToAG_IAS_TEMP_PAYMENT_DETAIL_HIS(AG_IAS_TEMP_PAYMENT_DETAIL_HIS aG_IAS_TEMP_PAYMENT_DETAIL_HIS)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deprecated Method for adding a new object to the APPM_BUDGET_CODE EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToAPPM_BUDGET_CODE(APPM_BUDGET_CODE aPPM_BUDGET_CODE)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deprecated Method for adding a new object to the APPM_RECEIVE_GROUP EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToAPPM_RECEIVE_GROUP(APPM_RECEIVE_GROUP aPPM_RECEIVE_GROUP)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deprecated Method for adding a new object to the AG_IAS_EXAM_APPLICANT EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToAG_IAS_EXAM_APPLICANT(AG_IAS_EXAM_APPLICANT aG_IAS_EXAM_APPLICANT)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Deprecated Method for adding a new object to the AG_IAS_PAYMENT_FILE EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToAG_IAS_PAYMENT_FILE(AG_IAS_PAYMENT_FILE aG_IAS_PAYMENT_FILE)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deprecated Method for adding a new object to the AG_IAS_SUBPAYMENT_RECEIPT EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToAG_IAS_SUBPAYMENT_RECEIPT(AG_IAS_SUBPAYMENT_RECEIPT aG_IAS_SUBPAYMENT_RECEIPT)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Deprecated Method for adding a new object to the AG_IAS_PAYMENT_DETAIL EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToAG_IAS_PAYMENT_DETAIL(AG_IAS_PAYMENT_DETAIL aG_IAS_PAYMENT_DETAIL)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deprecated Method for adding a new object to the AG_IAS_PAYMENT_HEADER EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToAG_IAS_PAYMENT_HEADER(AG_IAS_PAYMENT_HEADER aG_IAS_PAYMENT_HEADER)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deprecated Method for adding a new object to the AG_IAS_PAYMENT_TOTAL EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToAG_IAS_PAYMENT_TOTAL(AG_IAS_PAYMENT_TOTAL aG_IAS_PAYMENT_TOTAL)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deprecated Method for adding a new object to the AG_IAS_PAYMENT_DETAIL_HIS EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToAG_IAS_PAYMENT_DETAIL_HIS(AG_IAS_PAYMENT_DETAIL_HIS aG_IAS_PAYMENT_DETAIL_HIS)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Deprecated Method for adding a new object to the AG_TRAIN_PERIOD_R EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToAG_TRAIN_PERIOD_R(AG_TRAIN_PERIOD_R aG_TRAIN_PERIOD_R)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deprecated Method for adding a new object to the AG_TRAIN_SPECIAL_USED_T EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToAG_TRAIN_SPECIAL_USED_T(AG_TRAIN_SPECIAL_USED_T aG_TRAIN_SPECIAL_USED_T)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deprecated Method for adding a new object to the AG_TRAIN_SPECIAL_DISCOUNT_T EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToAG_TRAIN_SPECIAL_DISCOUNT_T(AG_TRAIN_SPECIAL_DISCOUNT_T aG_TRAIN_SPECIAL_DISCOUNT_T)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deprecated Method for adding a new object to the AG_IAS_SCHEDULE EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToAG_IAS_SCHEDULE(AG_IAS_SCHEDULE aG_IAS_SCHEDULE)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Deprecated Method for adding a new object to the AG_AGENT_LICENSE_REINSURE_T EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToAG_AGENT_LICENSE_REINSURE_T(AG_AGENT_LICENSE_REINSURE_T aG_AGENT_LICENSE_REINSURE_T)
        {
            throw new NotImplementedException();
        }

        #endregion       
    }
}

