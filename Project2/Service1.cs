using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Project2
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
             //These variables manage credit card list 
            static Dictionary<string, double> bank_details = new Dictionary<string, double>();
            string encrypted_data;
            public void GetData()
            {
                bank_details.Clear();
            }
            
        //Bookstore can request for creditcard ,and also get an initial deposit amount
            public string CreateCreditCard(int bookstoreID)
            {
                //
                Random rnd = new Random();
                string credit_card_number = "1011" + bookstoreID.ToString()+ rnd.Next(111,999) + rnd.Next(1048, 8647).ToString() + rnd.Next(1111, 9999).ToString();
                bank_details.Add(credit_card_number, 100000.0);
                Console.WriteLine("Credit Card Added {0} with bank Balance {1}", credit_card_number,bank_details[credit_card_number]);
                return credit_card_number;
             }
            
            //Deposit money into account by providing valid credit card details
            public double DepositAmount(string creditcardnumber, double amount)
             {

                foreach (var entry in bank_details)
                {

                    if (entry.Key.Equals(creditcardnumber))
                    {
                        bank_details[entry.Key] = entry.Value + amount;
                        return bank_details[entry.Key];
                    }


                }
                Console.WriteLine("Retry again !!!Entry a valid Credit Card Number");
                return 0;
            }
             //Using enryption service to encrypt data
            public string encryptCreditCard(string creditcardnumber)
            {
                encrypted_data = "";
                encryptdecrypt.ServiceClient service_dec = new encryptdecrypt.ServiceClient();
                encrypted_data = service_dec.Encrypt(creditcardnumber);
                return encrypted_data;
            }

            //Check validity of Credit card by checking if it's already exisiting in the hashmap and also check balance
            public bool CheckValidTransaction(string encrypted_data, double amount)
            {
                encryptdecrypt.ServiceClient service_dec = new encryptdecrypt.ServiceClient();
                string decrypted_data = service_dec.Decrypt(encrypted_data);
                encrypted_data = "";
                foreach (var entry in bank_details)
                {
                
                if (entry.Key.Equals(decrypted_data))
                    {
                        if (entry.Value > amount)
                        {
                        //Console.WriteLine("Sufficient Funds {0}", entry.Value);
                            bank_details[entry.Key] = entry.Value - amount;
                        return true;
                        }

                    }


                }
            return false;

            }


        }
    }
