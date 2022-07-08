
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockPersonRepository : Mock<IPersonRepository>
    {
        public MockPersonRepository MockPeopleAdd(PeopleDTO res)
        {
            Setup(x => x.AddPerson(It.IsAny<PeopleDTO>()))
                 .Returns(res);
            return this;
        }

        public MockPersonRepository MockPeopleEditDetails(PeopleDTO result)
        {
            Setup(x => x.UpdatePerson(It.IsAny<PeopleDTO>()))
               .Returns(result);

            Setup(x => x.GetPerson(It.IsAny<Guid>()))
                 .Returns(result);
            return this;
        }
        public MockPersonRepository MockPeopleSupport(PeopleDTO peopleData, List<PeopleSupportDTO> peopleSupportData)
        {
            Setup(x => x.GetPerson(It.IsAny<Guid>()))
                 .Returns(peopleData);

            Setup(x => x.GetPeopleSupportList(It.IsAny<long>()))
                .Returns(peopleSupportData);
            return this;
        }

        public MockPersonRepository MockPersonData(PeopleDTO peopleData)
        {
            this.MockGetIsValidPersonInAgency(true);
            Setup(x => x.GetPerson(It.IsAny<Guid>()))
                 .Returns(peopleData);
            return this;
        }

        public MockPersonRepository MockGetNotifications(PeopleDTO peopleData, List<RiskNotificationsListDTO> riskNotifications, List<ReminderNotificationsListDTO> reminderNotifications)
        {
            Setup(x => x.GetPerson(It.IsAny<Guid>()))
                 .Returns(peopleData);

            Setup(x => x.GetRiskNotificationList(It.IsAny<long>()))
                 .Returns(riskNotifications);

            Setup(x => x.GetReminderNotificationList(It.IsAny<long>()))
                .Returns(reminderNotifications);
            return this;
        }

        public MockPersonRepository MockGetNotificationsException(PeopleDataDTO peopleData, List<RiskNotificationsListDTO> riskNotifications, List<ReminderNotificationsListDTO> reminderNotifications)
        {
            Setup(x => x.GetPeopleDetails(It.IsAny<Guid>()))
                 .Returns(peopleData);

            Setup(x => x.GetRiskNotificationList(It.IsAny<long>()))
                 .Throws<Exception>();

            Setup(x => x.GetReminderNotificationList(It.IsAny<long>()))
                .Returns(reminderNotifications);
            return this;
        }

        public MockPersonRepository MockGetPastNotifications(PeopleDTO peopleData, List<PastNotificationsListDTO> pastNotifications)
        {
            Setup(x => x.GetPerson(It.IsAny<Guid>()))
                 .Returns(peopleData);

            Setup(x => x.GetPastNotificationList(It.IsAny<long>()))
                 .Returns(pastNotifications);
            return this;
        }

        public MockPersonRepository MockGetPastNotificationsException(PeopleDataDTO peopleData, List<PastNotificationsListDTO> riskNotifications)
        {
            Setup(x => x.GetPeopleDetails(It.IsAny<Guid>()))
                 .Returns(peopleData);

            Setup(x => x.GetPastNotificationList(It.IsAny<long>()))
                 .Throws<Exception>();
            return this;
        }

        public MockPersonRepository MockGetPastNotes(List<NotificationNotesDTO> pastNotes)
        {

            Setup(x => x.GetAllPastNotes(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                 .Returns(pastNotes);
            return this;
        }

        public MockPersonRepository MockGetPastNotesException(List<NotificationNotesDTO> pastNotes)
        {
            Setup(x => x.GetAllPastNotes(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                 .Throws<Exception>();
            return this;
        }

        public MockPersonRepository MockPeopleGetDetails(PeopleDataDTO peopleData)
        {
            Setup(x => x.GetPeopleDetails(It.IsAny<Guid>()))
                  .Returns(peopleData);
            return this;
        }

        public MockPersonRepository MockGetPersonHelpingCount(List<PersonDTO> response)
        {
            Setup(x => x.GetPersonsListByHelperID(It.IsAny<PersonSearchDTO>(), It.IsAny<DynamicQueryBuilderDTO>()))
                .Returns(response);
            return this;
        }

        public MockPersonRepository MockGetPersonHelpingCountException()
        {
            Setup(x => x.GetPersonsListByHelperIDCount(It.IsAny<PersonSearchDTO>(), It.IsAny<DynamicQueryBuilderDTO>()))
            .Throws<Exception>();
            return this;
        }

        public MockPersonRepository MockGetPersonInitials(PersonInitialsDTO result)
        {
            Setup(x => x.GetPersonInitials(It.IsAny<Guid>()))
                .Returns(result);
            return this;
        }

        public MockPersonRepository MockGetPersonInitialsException(PersonInitialsDTO result)
        {
            Setup(x => x.GetPersonInitials(It.IsAny<Guid>()))
            .Throws<Exception>();
            return this;
        }
        public MockPersonRepository MockGetPerson(PeopleDTO result)
        {
            this.MockGetIsValidPersonInAgency(true);
            Setup(x => x.GetPerson(It.IsAny<Guid>()))
                 .Returns(result);
            return this;
        }

        public MockPersonRepository MockGetPersonIDS(PeopleDTO result1, SharedDetailsDTO result2)
        {
            Setup(x => x.GetPersonalDetails(It.IsAny<Guid>()))
                .Returns(result1);

            Setup(x => x.GetSharedPersonQuestionnaireDetails(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(result2);
            return this;
        }

        public MockPersonRepository MockGetPersonWithInitials(PeopleDTO result1, PersonInitialsDTO result2)
        {
            this.MockGetIsValidPersonInAgency(true);
            Setup(x => x.GetPerson(It.IsAny<Guid>()))
                 .Returns(result1);

            Setup(x => x.GetPersonInitials(It.IsAny<Guid>()))
                .Returns(result2);
            return this;
        }

        public MockPersonRepository MockGetPeopleCollaborationList(List<PeopleCollaborationDTO> result)
        {
            Setup(x => x.GetPeopleCollaborationList(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(result);
            return this;
        }

        public MockPersonRepository MockGetPeopleCollaborationListException(List<PeopleCollaborationDTO> result)
        {
            Setup(x => x.GetPeopleCollaborationList(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<int>(), It.IsAny<int>()))
            .Throws<Exception>();
            return this;
        }

        public MockPersonRepository MockGetPeopleDetails(PeopleDTO result, SharedDetailsDTO result2)
        {
            Setup(x => x.GetPersonalDetails(It.IsAny<Guid>()))
             .Returns(result);

            Setup(x => x.GetSharedPersonQuestionnaireDetails(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(result2);
            return this;
        }
        public MockPersonRepository MockGetPersonRow(Person result)
        {
            this.MockGetIsValidPersonInAgency(true);

            Setup(x => x.GetRowAsync(It.IsAny<Expression<Func<Person, bool>>>()))
                  .Returns(Task.FromResult(result));

            return this;
        }
        public MockPersonRepository MockGetPersonDetailsShared(Person result)
        {
            this.MockGetIsValidPersonInAgency(true);

            Setup(x => x.GetRowAsync(It.IsAny<Expression<Func<Person, bool>>>()))
                  .Returns(Task.FromResult(result));

            return this;
        }
        public MockPersonRepository MockGetIsValidPersonInAgency(bool result2)
        {
            Setup(x => x.IsValidPersonInAgency(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<long>()))
                .Returns(result2);
            Setup(x => x.IsValidPersonInAgency(It.IsAny<Guid>(), It.IsAny<long>()))
               .Returns(result2);

            Setup(x => x.IsValidPersonInAgencyForQuestionnaire(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<int>(), It.IsAny<long>(), It.IsAny<bool>()))
                .Returns(result2);
            Setup(x => x.IsValidPersonInAgencyForQuestionnaire(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<long>(), It.IsAny<bool>()))
               .Returns(result2);
            return this;
        }
    }
}
