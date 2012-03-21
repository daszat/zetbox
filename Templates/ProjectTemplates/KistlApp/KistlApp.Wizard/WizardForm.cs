
namespace KistlApp.Wizard
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Data.SqlClient;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using Npgsql;

    public partial class WizardForm : Form
    {
        public static readonly string SQLConnectionStringTemplate = @"Data Source=.\SQLEXPRESS;Initial Catalog={0};Integrated Security=True;MultipleActiveResultSets=true;";
        public static readonly string PGConnectionStringTemplate = @"Server=localhost;Port=5432;Database={0};User Id=zbox;Password=";

        public WizardForm(string solutionname)
        {
            InitializeComponent();

            this.SolutionName = solutionname;
            this.txtConnectionString.Text = string.Format(SQLConnectionStringTemplate, SolutionName);
        }

        public string SolutionName { get; set; }
        public string ConnectinString { get; set; }
        public string DatabaseName { get; set; }
        public string Schema { get; set; }
        public string Provider { get; set; }
        public string ORMapperClassName { get; set; }
        public string ORMapperModule { get; set; }

        private void btnNext_Click(object sender, EventArgs e)
        {
            ConnectinString = txtConnectionString.Text;
            ORMapperClassName = rbEF.Checked ? "Ef" : "NHibernate";
            ORMapperModule = rbEF.Checked ? "EF" : "NH";
            Schema = rbSQLServer.Checked ? "MSSQL" : "POSTGRESQL";
            Provider = rbSQLServer.Checked ? "NHibernate.Dialect.MsSql2005Dialect" : "Npgsql";

            if (rbSQLServer.Checked)
            {
                var cb = new SqlConnectionStringBuilder(txtConnectionString.Text);
                DatabaseName = cb.InitialCatalog;
            }
            else
            {
                var cb = new NpgsqlConnectionStringBuilder(txtConnectionString.Text);
                DatabaseName = cb.Database;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void rbSQLServer_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSQLServer.Checked)
            {
                txtConnectionString.Text = string.Format(SQLConnectionStringTemplate, SolutionName);
            }
        }

        private void rbPGSQL_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPGSQL.Checked)
            {
                txtConnectionString.Text = string.Format(PGConnectionStringTemplate, SolutionName);
            }
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtConnectionString.Text))
            {
                MessageBox.Show("Empty connection string");
                return;
            }
            try
            {
                if (rbSQLServer.Checked)
                {
                    SqlConnection.ClearAllPools();
                    using (var db = new SqlConnection(txtConnectionString.Text))
                    {
                        db.Open();
                    }
                }
                else
                {
                    NpgsqlConnection.ClearAllPools();
                    using (var db = new NpgsqlConnection(txtConnectionString.Text))
                    {
                        db.Open();
                    }
                }

                MessageBox.Show("Success", "Test connection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to database:\n" + ex.Message,
                    "Test connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCreateDatabase_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtConnectionString.Text))
            {
                MessageBox.Show("Empty connection string");
                return;
            }
            try
            {
                if (rbSQLServer.Checked)
                {
                    SqlConnection.ClearAllPools();
                    var cb = new SqlConnectionStringBuilder(txtConnectionString.Text);
                    var dbname = cb.InitialCatalog;
                    cb.InitialCatalog = "master";
                    using (var db = new SqlConnection(cb.ToString()))
                    {
                        db.Open();
                        var cmd = new SqlCommand(string.Format("CREATE DATABASE [{0}]", dbname), db);
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    NpgsqlConnection.ClearAllPools();
                    var cb = new NpgsqlConnectionStringBuilder(txtConnectionString.Text);
                    var dbname = cb.Database;
                    cb.Database = "postgres";
                    using (var db = new NpgsqlConnection(cb.ToString()))
                    {
                        db.Open();
                        var cmd = new NpgsqlCommand(string.Format("CREATE DATABASE [{0}];", dbname), db);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Success", "Test connection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to database:\n" + ex.Message,
                    "Test connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
