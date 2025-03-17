using CRUDAPI.Models;

namespace CRUDAPI.Repository
{
    public interface IContactRepository
    {
        public Task<IEnumerable<Contact>> GetAllContact();
        public Task<Contact> GetContactById(int ContactId);
        public Task CreateContact(Contact contact);
        public Task UpdateContact(Contact contact);
        public Task DeleteContact(int ContactId);
    }
}
