using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Project2
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        void GetData();

        [OperationContract]
        string CreateCreditCard(int bookstoreID);

        [OperationContract]
        bool CheckValidTransaction(string creditcardnumber, double amount);

        [OperationContract]
        string encryptCreditCard(string creditcardnumber);

        [OperationContract]
        double DepositAmount(string creditcardnumber, double amount);



        // TODO: Add your service operations here
    }
}
