using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Banking.DAO
{
    public class JarTransactionDAO //Giao dịch Hủ
    {
        private static JarTransactionDAO instance;

        public static JarTransactionDAO Instance
        {
            get { if (instance == null) { instance = new JarTransactionDAO(); } return instance; }
            private set { instance = value; }
        }
        private JarTransactionDAO() { }
    }
}
