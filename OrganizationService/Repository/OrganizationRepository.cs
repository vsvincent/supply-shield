using Amazon.IonDotnet.Tree.Impl;
using Amazon.IonDotnet.Tree;
using Amazon.QLDB.Driver;
using OrganizationService.Models;
using OrganizationService.Utils;
using Amazon.QLDB.Driver.Generic;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Collections.Generic;

namespace OrganizationService.Repository
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private IQldbContext _driver;
        public OrganizationRepository(IQldbContext driver)
        {
            _driver = driver;
        }

        public async void Add(IOrganization organization)
        {
            ValueFactory valueFactory = new ValueFactory();
            IIonValue ionOrganizationCode = valueFactory.NewString(organization.Code);

            IIonValue ionOrganization = valueFactory.NewEmptyStruct();
            ionOrganization.SetField("Code", ionOrganizationCode);
            ionOrganization.SetField("Name", valueFactory.NewString(organization.Name));
            await _driver.GetDriver().Execute(async txn =>
            {
                Amazon.QLDB.Driver.IResult result = txn.Execute("SELECT * FROM Organization WHERE Code = ?", ionOrganizationCode);
                int count = result.Count();
                if (count > 0)
                {
                    return;
                }
                txn.Execute("INSERT INTO Organization ?", ionOrganization);
            });
        }

        public async Task<IEnumerable<IOrganization>> GetAll()
        {
            
            IAsyncResult<Organization> result = await _driver.GetAsyncDriver().Execute(async txn =>
            {
                return await txn.Execute(txn.Query<Organization>("SELECT * FROM Organization"));
            });
            return await result.ToListAsync();
        }
    }
}
