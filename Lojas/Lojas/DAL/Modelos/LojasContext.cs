using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DAL.Modelos
{
    public partial class LojasContext : DbContext
    {
        public LojasContext()
        {
        }

        public LojasContext(DbContextOptions<LojasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cadastro> Cadastros { get; set; }
        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Endereco> Enderecos { get; set; }
        public virtual DbSet<Fornecedore> Fornecedores { get; set; }
        public virtual DbSet<MovimentacoesEstoque> MovimentacoesEstoques { get; set; }
        public virtual DbSet<Perfil> Perfils { get; set; }
        public virtual DbSet<Produto> Produtos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=localhost\\SQLExpress;Initial Catalog=Lojas;Integrated Security=True;Encrypt=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Cadastro>(entity =>
            {
                entity.ToTable("Cadastro");

                entity.HasIndex(e => e.Email, "UQ__Cadastro__A9D105341731E4D1")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DataCadastro)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PasswordHash).HasMaxLength(255);

                entity.Property(e => e.Salt).HasMaxLength(255);
            });

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.Property(e => e.CategoriaId).HasColumnName("CategoriaID");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Endereco>(entity =>
            {
                entity.ToTable("Endereco");

                entity.Property(e => e.Bairro).HasMaxLength(100);

                entity.Property(e => e.Cep)
                    .HasMaxLength(20)
                    .HasColumnName("CEP");

                entity.Property(e => e.Cidade).HasMaxLength(100);

                entity.Property(e => e.Complemento).HasMaxLength(50);

                entity.Property(e => e.Estado).HasMaxLength(50);

                entity.Property(e => e.Numero).HasMaxLength(10);

                entity.Property(e => e.Rua).HasMaxLength(200);

                entity.HasOne(d => d.Perfil)
                    .WithMany(p => p.Enderecos)
                    .HasForeignKey(d => d.PerfilId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Endereco__Perfil__6A70BD6B");
            });

            modelBuilder.Entity<Fornecedore>(entity =>
            {
                entity.HasKey(e => e.FornecedorId)
                    .HasName("PK__Forneced__494B8C3020C9149C");

                entity.Property(e => e.FornecedorId).HasColumnName("FornecedorID");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Telefone)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MovimentacoesEstoque>(entity =>
            {
                entity.HasKey(e => e.MovimentacaoId)
                    .HasName("PK__Moviment__509C01B5CE029178");

                entity.ToTable("MovimentacoesEstoque");

                entity.Property(e => e.MovimentacaoId).HasColumnName("MovimentacaoID");

                entity.Property(e => e.DataMovimentacao)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ProdutoId).HasColumnName("ProdutoID");

                entity.Property(e => e.TipoMovimentacao)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Produto)
                    .WithMany(p => p.MovimentacoesEstoques)
                    .HasForeignKey(d => d.ProdutoId)
                    .HasConstraintName("FK__Movimenta__Produ__65AC084E");
            });

            modelBuilder.Entity<Perfil>(entity =>
            {
                entity.ToTable("Perfil");

                entity.Property(e => e.Cpf)
                    .HasMaxLength(450)
                    .HasColumnName("CPF");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Produto>(entity =>
            {
                entity.Property(e => e.ProdutoId).HasColumnName("ProdutoID");

                entity.Property(e => e.CategoriaId).HasColumnName("CategoriaID");

                entity.Property(e => e.FornecedorId).HasColumnName("FornecedorID");

                entity.Property(e => e.ImagemUrl)
                    .HasMaxLength(255)
                    .HasColumnName("ImagemURL");

                entity.Property(e => e.LinkCompra).HasMaxLength(255);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Preco).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.QuantidadeEstoque).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Categoria)
                    .WithMany(p => p.Produtos)
                    .HasForeignKey(d => d.CategoriaId)
                    .HasConstraintName("FK__Produtos__Catego__5FF32EF8");

                entity.HasOne(d => d.Fornecedor)
                    .WithMany(p => p.Produtos)
                    .HasForeignKey(d => d.FornecedorId)
                    .HasConstraintName("FK__Produtos__Fornec__60E75331");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
