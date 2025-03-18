using System.Text.RegularExpressions;
using FIAP.TC.Fase03.ContatosAPI.Remocao.Domain.Interfaces;

namespace FIAP.TC.Fase03.ContatosAPI.Atualizacao.Domain.Entity;

     public class Contato : Remocao.Domain.Entity.Entity, IAggregateRoot
    {
        public string Nome { get; private set; }
        public string Telefone { get; private set; }
        public string Email { get; private set; }
        public string Ddd { get; private set; }
        
        public Contato() {}
        public Contato(string nome, string telefone, string email, string ddd)
        {
            Nome = nome;
            Telefone = telefone;
            Email = email;
            Ddd = ddd;
            
            Validate();
        }
        
        public void Atualizar(string nome, string telefone, string email, string ddd)
        {
            Nome = nome;
            Telefone = telefone;
            Email = email;
            Ddd = ddd;
            
            Validate();
        }
        
        private void Validate()
        {
            
            if (string.IsNullOrWhiteSpace(Nome))
                throw new ArgumentException("O nome é obrigatório.");
            if (Nome.Length < 2 || Nome.Length > 100)
                throw new ArgumentException("O nome deve ter entre 2 e 100 caracteres.");

            
            if (string.IsNullOrWhiteSpace(Telefone))
                throw new ArgumentException("O telefone é obrigatório.");
            if (!Regex.IsMatch(Telefone, @"^\d{8,11}$"))
                throw new ArgumentException("O telefone deve conter entre 8 e 11 dígitos numéricos.");
            
            if (string.IsNullOrWhiteSpace(Email))
                throw new ArgumentException("O e-mail é obrigatório.");
            if (!IsValidEmail(Email))
                throw new ArgumentException("O e-mail informado é inválido.");
            
            if (string.IsNullOrWhiteSpace(Ddd))
                throw new ArgumentException("O DDD é obrigatório.");
            if (!Regex.IsMatch(Ddd, @"^\d{2}$"))
                throw new ArgumentException("O DDD deve conter exatamente 2 dígitos numéricos.");
        }
        
        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase);
        }
        
        
        
        public override string ToString()
        {
            return $"Contato: {Nome}, Telefone: ({Ddd}) {Telefone}, Email: {Email}";
        }
        
        public override bool Equals(object obj)
        {
            if (obj is Contato contato)
            {
                return Email.Equals(contato.Email, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Email.GetHashCode(StringComparison.OrdinalIgnoreCase);
        }
    }
