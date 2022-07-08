
using Microsoft.Extensions.Localization;
using Moq;
using Opeeka.PICS.Domain.Resources;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockLocalize
    {
        // localization mock class
        public Mock<LocalizeService> Localize(string returnValue)
        {
            Mock<IStringLocalizerFactory> stringLocalizer = new Mock<IStringLocalizerFactory>();
            Mock<LocalizeService> localize = new Mock<LocalizeService>(stringLocalizer.Object);
            localize.Setup(x => x.GetLocalizedHtmlString(It.IsAny<string>()))
                             .Returns(new LocalizedString(returnValue, returnValue));
            return localize;
        }
        public Mock<LocalizeService> Localize()
        {
            Mock<IStringLocalizerFactory> stringLocalizer = new Mock<IStringLocalizerFactory>();
            Mock<LocalizeService> localize = new Mock<LocalizeService>(stringLocalizer.Object);
            localize.Setup(x => x.GetLocalizedHtmlString(It.IsAny<string>()));
            return localize;
        }
    }
}
