using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Compuletra.RestApiExample.Data;
using Compuletra.RestApiExample.Models;
using Compuletra.RestApiExample.Test.Setup;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Compuletra.RestApiExample.Test.Controllers {
    public class VeiculoResourceIntTest {
        public VeiculoResourceIntTest()
        {
            _factory = new NhipsterWebApplicationFactory<TestStartup>().WithMockUser();
            _client = _factory.CreateClient();

            _applicationDatabaseContext = _factory.GetRequiredService<ApplicationDatabaseContext>();

            InitTest();
        }

        private const string DefaultPlaca = "AAAAAAAAAA";
        private const string UpdatedPlaca = "BBBBBBBBBB";

        private const string DefaultCor = "AAAAAAAAAA";
        private const string UpdatedCor = "BBBBBBBBBB";

        private const string DefaultTipo = "AAAAAAAAAA";
        private const string UpdatedTipo = "BBBBBBBBBB";

        private readonly NhipsterWebApplicationFactory<TestStartup> _factory;
        private readonly HttpClient _client;

        private readonly ApplicationDatabaseContext _applicationDatabaseContext;

        private Veiculo _veiculo;

        private Veiculo CreateEntity()
        {
            return new Veiculo {
                Placa = DefaultPlaca,
                Cor = DefaultCor,
                Tipo = DefaultTipo
            };
        }

        private void InitTest()
        {
            _veiculo = CreateEntity();
        }

        [Fact]
        public async Task CreateVeiculo()
        {
            var databaseSizeBeforeCreate = _applicationDatabaseContext.Veiculos.Count();

            // Create the Veiculo
            var response = await _client.PostAsync("/api/veiculos", TestUtil.ToJsonContent(_veiculo));
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            // Validate the Veiculo in the database
            var veiculoList = _applicationDatabaseContext.Veiculos.ToList();
            veiculoList.Count().Should().Be(databaseSizeBeforeCreate + 1);
            var testVeiculo = veiculoList[veiculoList.Count - 1];
            testVeiculo.Placa.Should().Be(DefaultPlaca);
            testVeiculo.Cor.Should().Be(DefaultCor);
            testVeiculo.Tipo.Should().Be(DefaultTipo);
        }

        [Fact]
        public async Task CreateVeiculoWithExistingId()
        {
            var databaseSizeBeforeCreate = _applicationDatabaseContext.Veiculos.Count();
            databaseSizeBeforeCreate.Should().Be(0);
            // Create the Veiculo with an existing ID
            _veiculo.Id = 1L;

            // An entity with an existing ID cannot be created, so this API call must fail
            var response = await _client.PostAsync("/api/veiculos", TestUtil.ToJsonContent(_veiculo));
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            // Validate the Veiculo in the database
            var veiculoList = _applicationDatabaseContext.Veiculos.ToList();
            veiculoList.Count().Should().Be(databaseSizeBeforeCreate);
        }

        [Fact]
        public async Task CheckPlacaIsRequired()
        {
            var databaseSizeBeforeTest = _applicationDatabaseContext.Veiculos.Count();

            // Set the field to null
            _veiculo.Placa = null;

            // Create the Veiculo, which fails.
            var response = await _client.PostAsync("/api/veiculos", TestUtil.ToJsonContent(_veiculo));
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var veiculoList = _applicationDatabaseContext.Veiculos.ToList();
            veiculoList.Count().Should().Be(databaseSizeBeforeTest);
        }

        [Fact]
        public async Task CheckCorIsRequired()
        {
            var databaseSizeBeforeTest = _applicationDatabaseContext.Veiculos.Count();

            // Set the field to null
            _veiculo.Cor = null;

            // Create the Veiculo, which fails.
            var response = await _client.PostAsync("/api/veiculos", TestUtil.ToJsonContent(_veiculo));
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var veiculoList = _applicationDatabaseContext.Veiculos.ToList();
            veiculoList.Count().Should().Be(databaseSizeBeforeTest);
        }

        [Fact]
        public async Task CheckTipoIsRequired()
        {
            var databaseSizeBeforeTest = _applicationDatabaseContext.Veiculos.Count();

            // Set the field to null
            _veiculo.Tipo = null;

            // Create the Veiculo, which fails.
            var response = await _client.PostAsync("/api/veiculos", TestUtil.ToJsonContent(_veiculo));
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var veiculoList = _applicationDatabaseContext.Veiculos.ToList();
            veiculoList.Count().Should().Be(databaseSizeBeforeTest);
        }

        [Fact]
        public async Task GetAllVeiculos()
        {
            // Initialize the database
            _applicationDatabaseContext.Veiculos.Add(_veiculo);
            await _applicationDatabaseContext.SaveChangesAsync();

            // Get all the veiculoList
            var response = await _client.GetAsync("/api/veiculos?sort=id,desc");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var json = JToken.Parse(await response.Content.ReadAsStringAsync());
            json.SelectTokens("$.[*].id").Should().Contain(_veiculo.Id);
            json.SelectTokens("$.[*].placa").Should().Contain(DefaultPlaca);
            json.SelectTokens("$.[*].cor").Should().Contain(DefaultCor);
            json.SelectTokens("$.[*].tipo").Should().Contain(DefaultTipo);
        }

        [Fact]
        public async Task GetVeiculo()
        {
            // Initialize the database
            _applicationDatabaseContext.Veiculos.Add(_veiculo);
            await _applicationDatabaseContext.SaveChangesAsync();

            // Get the veiculo
            var response = await _client.GetAsync($"/api/veiculos/{_veiculo.Id}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var json = JToken.Parse(await response.Content.ReadAsStringAsync());
            json.SelectTokens("$.id").Should().Contain(_veiculo.Id);
            json.SelectTokens("$.placa").Should().Contain(DefaultPlaca);
            json.SelectTokens("$.cor").Should().Contain(DefaultCor);
            json.SelectTokens("$.tipo").Should().Contain(DefaultTipo);
        }

        [Fact]
        public async Task GetNonExistingVeiculo()
        {
            var response = await _client.GetAsync("/api/veiculos/" + long.MaxValue);
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdateVeiculo()
        {
            // Initialize the database
            _applicationDatabaseContext.Veiculos.Add(_veiculo);
            await _applicationDatabaseContext.SaveChangesAsync();

            var databaseSizeBeforeUpdate = _applicationDatabaseContext.Veiculos.Count();

            // Update the veiculo
            var updatedVeiculo =
                await _applicationDatabaseContext.Veiculos.SingleOrDefaultAsync(it => it.Id == _veiculo.Id);
            // Disconnect from session so that the updates on updatedVeiculo are not directly saved in db
//TODO detach
            updatedVeiculo.Placa = UpdatedPlaca;
            updatedVeiculo.Cor = UpdatedCor;
            updatedVeiculo.Tipo = UpdatedTipo;

            var response = await _client.PutAsync("/api/veiculos", TestUtil.ToJsonContent(updatedVeiculo));
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // Validate the Veiculo in the database
            var veiculoList = _applicationDatabaseContext.Veiculos.ToList();
            veiculoList.Count().Should().Be(databaseSizeBeforeUpdate);
            var testVeiculo = veiculoList[veiculoList.Count - 1];
            testVeiculo.Placa.Should().Be(UpdatedPlaca);
            testVeiculo.Cor.Should().Be(UpdatedCor);
            testVeiculo.Tipo.Should().Be(UpdatedTipo);
        }

        [Fact]
        public async Task UpdateNonExistingVeiculo()
        {
            var databaseSizeBeforeUpdate = _applicationDatabaseContext.Veiculos.Count();

            // If the entity doesn't have an ID, it will throw BadRequestAlertException
            var response = await _client.PutAsync("/api/veiculos", TestUtil.ToJsonContent(_veiculo));
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            // Validate the Veiculo in the database
            var veiculoList = _applicationDatabaseContext.Veiculos.ToList();
            veiculoList.Count().Should().Be(databaseSizeBeforeUpdate);
        }

        [Fact]
        public async Task DeleteVeiculo()
        {
            // Initialize the database
            _applicationDatabaseContext.Veiculos.Add(_veiculo);
            await _applicationDatabaseContext.SaveChangesAsync();

            var databaseSizeBeforeDelete = _applicationDatabaseContext.Veiculos.Count();

            var response = await _client.DeleteAsync($"/api/veiculos/{_veiculo.Id}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // Validate the database is empty
            var veiculoList = _applicationDatabaseContext.Veiculos.ToList();
            veiculoList.Count().Should().Be(databaseSizeBeforeDelete - 1);
        }

        [Fact]
        public void EqualsVerifier()
        {
            TestUtil.EqualsVerifier(typeof(Veiculo));
            var veiculo1 = new Veiculo {
                Id = 1L
            };
            var veiculo2 = new Veiculo {
                Id = veiculo1.Id
            };
            veiculo1.Should().Be(veiculo2);
            veiculo2.Id = 2L;
            veiculo1.Should().NotBe(veiculo2);
            veiculo1.Id = 0;
            veiculo1.Should().NotBe(veiculo2);
        }
    }
}
