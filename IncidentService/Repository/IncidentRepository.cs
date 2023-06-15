using Amazon.IonDotnet.Tree.Impl;
using Amazon.IonDotnet.Tree;
using Amazon.QLDB.Driver;
using IncidentService.Models;
using IncidentService.Utils;
using Amazon.QLDB.Driver.Generic;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Collections.Generic;
using Amazon.IonObjectMapper;
using Amazon.IonDotnet;

namespace IncidentService.Repository
{
    public class IncidentRepository : IIncidentRepository
    {
        private IQldbContext _driver;
        public IncidentRepository(IQldbContext driver)
        {
            _driver = driver;
        }

        public async void Add(IIncident incident)
        {
            ValueFactory valueFactory = new ValueFactory();

            IIonValue ionIncident = valueFactory.NewEmptyStruct();
            ionIncident.SetField("Id", valueFactory.NewString(Guid.NewGuid().ToString()));
            ionIncident.SetField("Type", valueFactory.NewString(incident.Type));
            ionIncident.SetField("Description", valueFactory.NewString(incident.Description));
            ionIncident.SetField("User-Id", valueFactory.NewString(incident.UserId));
            ionIncident.SetField("Organization-Id", valueFactory.NewString(incident.OrganizationId));
            ionIncident.SetField("Date", valueFactory.NewTimestamp(new Timestamp(DateTime.Now)));
            await _driver.GetDriver().Execute(async txn =>
            {
                txn.Execute("INSERT INTO Incident ?", ionIncident);
            });
        }

        public async Task<IEnumerable<IIncident>> Get(string organizationId)
        {
            IAsyncResult<Incident> result = await _driver.GetAsyncDriver().Execute(async txn =>
            {
                return await txn.Execute(txn.Query<Incident>($"SELECT * FROM Incident WHERE \"Organization-Id\" = '{organizationId}'"));
            });
            return await result.ToListAsync();
        }

        public async Task<IEnumerable<IIncident>> GetAll()
        {
            IAsyncResult<Incident> result = await _driver.GetAsyncDriver().Execute(async txn =>
            {
                return await txn.Execute(txn.Query<Incident>("SELECT * FROM Incident"));
            });
            return await result.ToListAsync();
        }
    }
}
