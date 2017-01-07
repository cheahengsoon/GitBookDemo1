using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GitBookDemo
{
    public class MemberManager
    {
        IMobileServiceTable<Member> usersTable;
        MobileServiceClient client;

        public MemberManager()
        {
            client = new MobileServiceClient(Constants.ApplicationURL);

            usersTable = client.GetTable<Member>();
        }
        public async Task<Member> GetUserWhere(Expression<Func<Member, bool>> linq)
        {
            try
            {
                List<Member> newUser = await usersTable.Where(linq).Take(1).ToListAsync();
                return newUser.First();
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"INVALID {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"ERROR {0}", e.Message);
            }

            return null;
        }

        public async Task<Member> SaveGetUserAsync(Member user)
        {
            if (user.ID == null)
            {
                await usersTable.InsertAsync(user);
            }
            else
            {
                await usersTable.UpdateAsync(user);
            }

            try
            {
                List<Member> newUser = await usersTable.Where(userSelect => userSelect.username == user.username).ToListAsync();
                return newUser.First();
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"INVALID {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"ERROR {0}", e.Message);
            }

            return null;
        }

        public async Task<List<Member>> ListUserWhere(Expression<Func<Member, bool>> linq)
        {
            try
            {
                return new List<Member>
                (
                    await usersTable.Where(linq).ToListAsync()
                );
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"INVALID {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"ERROR {0}", e.Message);
            }
            return null;
        }
    }
}
