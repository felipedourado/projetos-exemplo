using Easynvest.Extrato.Configurations;
using Easynvest.Extrato.Services;
using Easynvest.Extrato.Services.Interfaces;
using Easynvest.ExtratoDal.Dtos;
using Easynvest.ExtratoDal.Infrastructure;
using Easynvest.ExtratoDal.Services.Interfaces;
using Moq;
using NUnit.Framework;
using System;

namespace Easynvest.Extrato.Testes
{
    public class ExtratoConsolidadoTests
    {
        private ExtratoService extratoService;
        private Mock<IHttpService> httpService;
        private Mock<IFundoServiceDal> fundoServiceMock;
        private Mock<ITesouroServiceDal> tesouroServiceMock;
        private Mock<ILciServiceDal> lciServiceMock;
        private Mock<FeaturesToogles> featureMock;


        [SetUp]
        public void Setup()
        {
            httpService = new Mock<IHttpService>();
            fundoServiceMock = new Mock<IFundoServiceDal>();
            tesouroServiceMock = new Mock<ITesouroServiceDal>();
            lciServiceMock = new Mock<ILciServiceDal>();
            featureMock = new Mock<FeaturesToogles>();

            extratoService = new ExtratoService(fundoServiceMock.Object, tesouroServiceMock.Object,
                lciServiceMock.Object, featureMock.Object);

            //settar os service mock com fake object

        }

        [Test]
        public void ExtratoConsolidado()
        {
            featureMock.Object.FundoEnable = false;
            featureMock.Object.TesouroEnable = true;
            featureMock.Object.LciEnable = true;

            httpService.Setup(x => x.Get<FundoDtoOutput>)

            extratoService.ExtratoConsolidado();

            fundoServiceMock.Verify(w => w.Consultar(), Times.Never);
            tesouroServiceMock.Verify(w => w.Consultar(), Times.Once);
            lciServiceMock.Verify(w => w.Consultar(), Times.Once);

            //var exception = Assert.Throws<ArgumentException>(() => extratoService.ExtratoConsolidado());

            Assert.Pass();
        }

        //fazer o teste de timeout
        //fazer o teste de httpexception not found
        //fazer o teste de conversão errada
        //fazer o teste de um feature toogle desligado

    }
}