using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APISwaggerNeo4j.Models
{
    /// <summary>
    /// Domain Model
    /// </summary>
    public class Domain
    {
        #region Private Properties

        private string _ip;
        private string _name;

        #endregion

        #region Constructor / Destructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="name"></param>
        public Domain(string ip, string name)
        {
            this._ip = ip;
            this._name = name;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Get / Set IP Address
        /// </summary>
        public string IP
        {
            get { return this._ip; }
            set { this._ip = value; }
        }

        /// <summary>
        /// Get / Set Name
        /// </summary>
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        #endregion

    }
}