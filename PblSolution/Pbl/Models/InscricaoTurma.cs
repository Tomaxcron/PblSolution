//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Pbl.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class InscricaoTurma
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InscricaoTurma()
        {
            this.ControleNotas = new HashSet<ControleNotas>();
            this.Grupo = new HashSet<Grupo>();
        }
    
        public int idInscricaoTurma { get; set; }
        public Nullable<int> idTurma { get; set; }
        public Nullable<int> idAluno { get; set; }
        public bool ativo { get; set; }
    
        public virtual Aluno Aluno { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ControleNotas> ControleNotas { get; set; }
        public virtual Turma Turma { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Grupo> Grupo { get; set; }
    }
}
