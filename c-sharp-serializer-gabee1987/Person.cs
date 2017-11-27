using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace c_sharp_serializer_gabee1987
{
    [Serializable]
    public class Person : ISerializable, IDeserializationCallback
    {
        #region Fields and Properties

        public enum Genders { Male, Female, Unknown };
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime CreationDate { get; set; }
        [NonSerialized] private int serialNumber;
        [NonSerialized] static int serialNumberCounter;

        #endregion

        #region Constructors

        public Person() { }

        public Person(string name, string address, string phoneNumber, DateTime creationDate)
        {
            this.Name = name;
            this.Address = address;
            this.PhoneNumber = phoneNumber;
            this.CreationDate = creationDate;
            SetSerialNumber();
            serialNumberCounter++;
        }

        #endregion

        #region Basic Methods

        public void SetSerialNumber()
        {
            this.serialNumber = serialNumber + 1;
        }

        public int GetSerialNumber()
        {
            return serialNumber;
        } 

        public override String ToString()
        {
            return "Name: " + Name + "\n" +
                    "Phone number: " + PhoneNumber + "\n" +
                    "Address: " + Address + "\n";
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            Person person = obj as Person;
            if ((System.Object)person == null)
            {
                return false;
            }
            return (Name == person.Name) &&
                (PhoneNumber == person.PhoneNumber) &&
                (Address == person.Address) &&
                (CreationDate == person.CreationDate) &&
                (serialNumber == person.serialNumber);
        }

        public bool Equals(Person person)
        {
            if ((object)person == null)
                return false;

            return (Name == person.Name) &&
                (PhoneNumber == person.PhoneNumber) &&
                (Address == person.Address) &&
                (CreationDate == person.CreationDate) &&
                (serialNumber == person.serialNumber);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^
                Address.GetHashCode() ^
                PhoneNumber.GetHashCode();
        }

        #endregion

        #region Serialization Methods

        void IDeserializationCallback.OnDeserialization(Object sender)
        {
            //// After being deserialized, initialize the serialNumber field 

            
        }

        // This method is to serialize data. The method is called 
        // on serialization.
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Use the AddValue method to specify serialized values.
            info.AddValue("Name", Name, typeof(string));
            info.AddValue("PhoneNumber", PhoneNumber, typeof(string));
            info.AddValue("Address", Address, typeof(string));
            info.AddValue("CreationDate", CreationDate, typeof(DateTime));
        }

        // The special constructor is used to deserialize values.
        public Person(SerializationInfo info, StreamingContext context)
        {
            // Reset the property value using the GetValue method.
            Name = (string)info.GetValue("Name", typeof(string));
            PhoneNumber = (string)info.GetValue("PhoneNumber", typeof(string));
            Address = (string)info.GetValue("Address", typeof(string));
            CreationDate = (DateTime)info.GetValue("CreationDate", typeof(DateTime));
        }

        public static void SerializePerson(Person personToSerialize, string output)
        {
            // Create file to save the data
            Stream writeStream = new FileStream(output, FileMode.Create, FileAccess.Write, FileShare.None);
            // Create and use a BinaryFormatter object to perform the serialization
            IFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(writeStream, personToSerialize);
            }
            catch (SerializationException se)
            {
                Console.WriteLine("Failed to serialize. Reason: " + se.Message);
                throw;
            }
            finally
            {
                // Close the file
                writeStream.Close();
            }
        }

        public static Person DeserializePerson(string fileNameInput)
        {
            try
            {
                // Open file to read the data from
                Stream openStream = new FileStream(fileNameInput, FileMode.Open, FileAccess.Read, FileShare.Read);
                // Create a BinaryFormatter object to perform the deserialization
                IFormatter formatter = new BinaryFormatter();
                // Use the BinaryFormatter object to deserialize the data from the file
                Person deserializedPerson = (Person)formatter.Deserialize(openStream);
                openStream.Close();
                return deserializedPerson;
            }
            catch (SerializationException se)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + se.Message);
                throw;
            }
            catch (FileNotFoundException fe)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + fe.Message);
            }
            return null;
        }


        #endregion
    }
}
