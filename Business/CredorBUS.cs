using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business
{
    public class CredorBUS
    {
        CredorDAL dal = new CredorDAL();
        public List<Credor> Credor()
        {
            List<Credor> lst = dal.Credor();
            return lst;
        }
    }
}
