using CRUDAPI.Context;
using CRUDAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDAPI.Repository
{
    public class ContactRepository(AppDbContext _appDbContext) : IContactRepository
    {
        private readonly AppDbContext appDbContext = _appDbContext;

        public async Task CreateContact(Contact contact)
        {
            try
            {
                appDbContext.Add(contact);
                await appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new Exception("Erro ao cadastrar contato.");
            }
        }

        public async Task DeleteContact(int ContactId)
        {
            try
            {
                var preEnroll = await GetContactById(ContactId);

                appDbContext.Remove(preEnroll);
                await appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new Exception("Erro ao deletar contato.");
            }
        }

        public async Task<IEnumerable<Contact>> GetAllContact()
        {
            try
            {
                var query = await(from C in appDbContext.Contacts select C).AsNoTracking().ToListAsync();

                return query;
            }
            catch (Exception)
            {
                throw new Exception("Erro ao consultar os pré-registros.");
            }
        }

        public async Task<Contact> GetContactById(int ContactId)
        {
            try
            {
                var query = await (from C in appDbContext.Contacts
                                   where C.Id == ContactId
                                   select C
                                    )
                                    .FirstOrDefaultAsync();

                return query;
            }
            catch (Exception)
            {
                throw new Exception("Erro ao consultar os pré-registros.");
            }
        }

        public async Task UpdateContact(Contact contact)
        {
            try
            {
                var existingContact = await GetContactById(contact.Id);

                if (existingContact == null)
                {
                    throw new Exception("Contato não encontrado.");
                }

                existingContact.Name = contact.Name;
                existingContact.Email = contact.Email;
                existingContact.PhoneNumber = contact.PhoneNumber;

                appDbContext.Update(existingContact);
                await appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new Exception("Erro ao atualizar contato.");
            }
        }
    }
}
