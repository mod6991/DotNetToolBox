using DotNetToolBox.Database;
using DotNetToolBox.IO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace DotNetToolBox.Tester
{
    public static class DbManagerTest
    {
        public static void Test()
        {
            try
            {
                DbManager db = new DbManager("Data Source=ARGAN;Persist Security Info=True;User ID=serviceuser;Password=svcnagra", "System.Data.OracleClient");
                db.Open();

                List<DbParameter> parameters = new List<DbParameter>();
                parameters.Add(db.CreateParameter("billcode", "502017832"));

                string request = "select * from commun.clients where billcode = :billcode";

                //Fill datatable
                DataTable dt = new DataTable();
                db.FillDataTableWithRequest(request, parameters, dt);

                StreamHelper.WriteString("<?xml version=\"1.0\" encoding=\"utf-8\" ?><Requests><Request Name=\"FillClients\">select * from commun.clients where billcode = :billcode</Request></Requests>", @"C:\Temp\requests.xml", Encoding.UTF8);

                //Fill dbobject with external request
                db.AddRequestFile("Clients", @"C:\Temp\requests.xml");
                List<DbParameter> parameters2 = new List<DbParameter>();
                parameters2.Add(db.CreateParameter("billcode", "502017832"));
                db.RegisterDbObject(typeof(Client));
                List<Client> clients = db["Clients"].FillObjects<Client>("FillClients", parameters2);

                db.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }

    public class Client : IDbObject
    {
        private Decimal _idClient;
        private String _name;
        private String _acr;
        private String _nom;
        private String _libelleCourt;
        private String _codeBanque;
        private Decimal _idProcesseur;
        private Byte[] _logo;
        private Byte[] _icon;
        private Decimal _billcode;
        private Decimal _idkdp;
        private Decimal _idClientPrecix;

        #region Properties

        public Decimal IdClient
        {
            get { return _idClient; }
            set { _idClient = value; }
        }

        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public String Acr
        {
            get { return _acr; }
            set { _acr = value; }
        }

        public String Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }

        public String LibelleCourt
        {
            get { return _libelleCourt; }
            set { _libelleCourt = value; }
        }

        public String CodeBanque
        {
            get { return _codeBanque; }
            set { _codeBanque = value; }
        }

        public Decimal IdProcesseur
        {
            get { return _idProcesseur; }
            set { _idProcesseur = value; }
        }

        public Byte[] Logo
        {
            get { return _logo; }
            set { _logo = value; }
        }

        public Byte[] Icon
        {
            get { return _icon; }
            set { _icon = value; }
        }

        public Decimal Billcode
        {
            get { return _billcode; }
            set { _billcode = value; }
        }

        public Decimal Idkdp
        {
            get { return _idkdp; }
            set { _idkdp = value; }
        }

        public Decimal IdClientPrecix
        {
            get { return _idClientPrecix; }
            set { _idClientPrecix = value; }
        }

        #endregion

        public List<DbObjectMapping> GetMapping()
        {
            List<DbObjectMapping> mapping = new List<DbObjectMapping>();
            mapping.Add(new DbObjectMapping("IdClient", "ID_CLIENT"));
            mapping.Add(new DbObjectMapping("Name", "NAME"));
            mapping.Add(new DbObjectMapping("Acr", "ACR"));
            mapping.Add(new DbObjectMapping("Nom", "NOM"));
            mapping.Add(new DbObjectMapping("LibelleCourt", "LIBELLE_COURT"));
            mapping.Add(new DbObjectMapping("CodeBanque", "CODE_BANQUE"));
            mapping.Add(new DbObjectMapping("IdProcesseur", "ID_PROCESSEUR"));
            mapping.Add(new DbObjectMapping("Logo", "LOGO"));
            mapping.Add(new DbObjectMapping("Icon", "ICON"));
            mapping.Add(new DbObjectMapping("Billcode", "BILLCODE"));
            mapping.Add(new DbObjectMapping("Idkdp", "IDKDP"));
            mapping.Add(new DbObjectMapping("IdClientPrecix", "ID_CLIENT_PRECIX"));
            return mapping;
        }
    }
}
