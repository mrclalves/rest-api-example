using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Compuletra.RestApiExample.Models {
    [Table("veiculo")]
    public class Veiculo {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public string Placa { get; set; }

        [Required]
        public string Cor { get; set; }

        [Required]
        public string Tipo { get; set; }

        // jhipster-needle-entity-add-field - JHipster will add fields here, do not remove

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var veiculo = obj as Veiculo;
            if (veiculo?.Id == null || veiculo?.Id == 0 || Id == 0) return false;
            return EqualityComparer<long>.Default.Equals(Id, veiculo.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public override string ToString()
        {
            return "Veiculo{" +
                    $"ID='{Id}'" +
                    $", Placa='{Placa}'" +
                    $", Cor='{Cor}'" +
                    $", Tipo='{Tipo}'" +
                    "}";
        }
    }
}
