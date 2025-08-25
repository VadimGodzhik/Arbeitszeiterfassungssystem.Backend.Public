using System;
using System.Collections;
using System.Collections.Generic;
using BRIT.Models.Entities;
//using BRIT.Models.Entities.Owned;
//using AutoLot.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Runtime.ConstrainedExecution;
using BRIT.Dal.Exceptions;
using BRIT.Models.ViewModels;

namespace BRIT.Dal.EfStructures
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //ChangeTracker.StateChanged += ChangeTracker_StateChanged;
            //ChangeTracker.Tracked += ChangeTracker_Tracked;
            base.SavingChanges += (sender, args) =>
            {
                Console.WriteLine($"Saving: Saving changes for {((ApplicationDbContext)sender)!.Database!.GetConnectionString()}");
                Console.WriteLine($"SavingChanges: Änderungen wurde für {((ApplicationDbContext)sender)!.Database!.ProviderName} gespeichert");

            };

            base.SavedChanges += (sender, args) =>
            {
                Console.WriteLine($"Saved: Saved {args!.EntitiesSavedCount} changes for {((ApplicationDbContext)sender)!.Database!.GetConnectionString()}");
                Console.WriteLine($"Saved: Änderungen wurde für {((ApplicationDbContext)sender)!.Database!.ProviderName} gespeichert");

            };
            base.SaveChangesFailed += (sender, args) =>
            {
                Console.WriteLine($"An exception occurred! {args.Exception.Message} entities");
                Console.WriteLine($"Eine Ausnahme ist aufgetreten! {args.Exception.Message} Entitäten");

            };
            
        }
        
        private void ChangeTracker_StateChanged(object? sender, EntityStateChangedEventArgs e)
        {
            if (e.Entry.Entity is not Angestellte a)
            {
                return;
            }

            var action = string.Empty;
            Console.WriteLine($"Angestellte {a.Vorname} {a.Nachname} was {e.OldState} before the state changed to {e.NewState}");
            switch (e.NewState)
            {
                case EntityState.Unchanged:
                    action = e.OldState switch
                    {
                        EntityState.Added => "Added",
                        EntityState.Modified => "Edited",
                        _ => action
                    };

                    Console.WriteLine($"The object was {action}");
                    break;
            }
        }
        

        
        private void ChangeTracker_Tracked(object? sender, EntityTrackedEventArgs e)
        {
            var source = (e.FromQuery) ? "Database" : "Code";
            if (e.Entry.Entity is Angestellte a)
            {
                Console.WriteLine($"Angestellte entry {a.Vorname} {a.Nachname} was added from {source}");
            }
        }
        

        public DbSet<Angestellte>? Angestelltes { get; set; }
        public DbSet<Rolle>? Rolles { get; set; }
        public DbSet<Arbeitszeiterfassung>? Arbeitszeiterfassungs { get; set; }
        public DbSet<Arbeitsandauer>? Arbeitsandauers { get; set; }
        public DbSet<Arbeitsort>? Arbeitsorts { get; set; }
        public DbSet<Fundort>? Fundorts { get; set; }
        public DbSet<Hausanschrift>? Hausanschrifts { get; set; }
        public DbSet<Kennwort>? Kennworts { get; set; }
        public DbSet<Stadt>? Stadts { get; set; }
        
        public DbSet<AngestellteArbeitsandauerViewModel>? AngestellteArbeitsandauerViewModels { get; set; }
        public DbSet<AngestellteArbeitszeiterfassungViewModel>? AngestellteArbeitszeiterfassungViewModels { get; set; }
        public DbSet<AngestellteArbeitsortViewModel>? AngestellteArbeitsortViewModels { get; set; }
        public DbSet<AngestellteRolleViewModel>? AngestellteRolleViewModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");
            /*
            modelBuilder.Entity<SeriLogEntry>(entity =>
            {
                entity.Property(e => e.Properties).HasColumnType("Xml");
                entity.Property(e => e.TimeStamp).HasDefaultValueSql("GetDate()");
            });
            */

            /*
            using (var context = new Context())
            {
                var persons = context.Persons.Join(
                    context.Addresses,
                    p => p.Id,
                    a => a.PersonId,
                    (persons, address) =>
                    new
                    {
                        PersonId = persons.Id,
                        FirstName = persons.Name,
                        LastName = persons.Surname,
                        AgeOfPerson = persons.Age,
                        Town = address.City,
                        Straße = address.Street
                    })
                    .OrderBy(p => p.FirstName)
                    .ThenBy(p => p.LastName);

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;

                foreach (var person in persons)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{person.PersonId}| {person?.FirstName} {person?.LastName} {person.AgeOfPerson} | Address: {person?.Town ?? "Münster"}, {person?.Straße}");
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.BackgroundColor = ConsoleColor.Black;

                // Agreagtion
                var data3 = context
                    .Persons
                    .GroupBy(p => p.Department.DepartmentId)
                    .Select(persons => new { key = persons.Key, count = persons.Count() })
                    ;

                foreach (var data in data3)
                {
                    Console.WriteLine($"Department {data.key} has {data.count} people");
                }
            }
            */
            modelBuilder.Entity<AngestellteArbeitsandauerViewModel>().HasNoKey().ToView("AngestellteArbeitsandauerView", "dbo");
            modelBuilder.Entity<AngestellteArbeitszeiterfassungViewModel>().HasNoKey().ToView("AngestellteArbeitszeiterfassungView", "dbo");
            modelBuilder.Entity<AngestellteArbeitsortViewModel>().HasNoKey().ToView("AngestellteArbeitsortView", "dbo");
            modelBuilder.Entity<AngestellteRolleViewModel>().HasNoKey().ToView("AngestellteRolleView", "dbo");

            modelBuilder.Entity<Angestellte>(entity =>
            {
                entity.HasQueryFilter(ang => ang.IstAngestellt);

                entity.Property(p => p.IstAngestellt).HasField("_istAngestellt").HasDefaultValue(true);

                entity.HasMany(ang => ang.Rolles)
                      .WithMany(r => r.Angestelltes)
                      .UsingEntity<Dictionary<string, object>>(
                      "AngestellteRollen",
                      j => j
                        .HasOne<Rolle>()
                        .WithMany()
                        .HasForeignKey("RolleId")
                        .HasConstraintName("FK_AngestellteRollen_Rolles_RolleId")
                        .OnDelete(DeleteBehavior.Cascade),
                      j => j
                        .HasOne<Angestellte>()
                        .WithMany()
                        .HasForeignKey("AngestellteId")
                        .HasConstraintName("FK_AngestellteRollen_Angestelltes_AngestellteId")
                        .OnDelete(DeleteBehavior.ClientCascade));

                entity.HasMany(ang => ang.Arbeitsorts)
                      .WithMany(ao => ao.Angestelltes)
                      .UsingEntity<Dictionary<string, object>>(
                      "AngestellteArbeitsorte",
                      j => j
                        .HasOne<Arbeitsort>()
                        .WithMany()
                        .HasForeignKey("ArbeitsortId")
                        .HasConstraintName("FK_AngestellteArbeitsorte_Arbeitsorte_ArbeitsortId")
                        .OnDelete(DeleteBehavior.Cascade),
                      j => j
                        .HasOne<Angestellte>()
                        .WithMany()
                        .HasForeignKey("AngestellteId")
                        .HasConstraintName("FK_AngestellteArbeitsorte_Angestellte_AngestellteId")
                        .OnDelete(DeleteBehavior.ClientCascade));

                entity.HasMany(e => e.Arbeitsandauers)
                    .WithOne(aand => aand.AngestellteNavigation!)
                    .HasForeignKey(aand => aand.AngestellteId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Angestellte_ArbeitsandauerId");

                entity.HasMany(e => e.Arbeitszeiterfassungs)
                    .WithOne(aze => aze.AngestellteNavigation!)
                    .HasForeignKey(aze => aze.AngestellteId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Angestellte_ArbeitszeiterfassungId");

            });

            modelBuilder.Entity<Kennwort>(entity =>
            {
                entity.HasIndex(e => e.AngestellteId, "IX_Kennwort_AngestellteId")
                      .IsUnique(true);
                entity.HasOne(k => k.AngestellteNavigation)
                      .WithOne(a => a.KennwortNavigation!)
                      .HasForeignKey<Kennwort>(k => k.AngestellteId);
            });

            modelBuilder.Entity<Hausanschrift>(entity =>
            {
                entity.HasIndex(e => e.AngestellteId, "IX_Hausanschrift_AngestellteId")
                      .IsUnique(true);
                entity.HasOne(ha => ha.AngestellteNavigation)
                      .WithOne(ang => ang.HausanschriftNavigation!)
                      .HasForeignKey<Hausanschrift>(ha => ha.AngestellteId);
            });

            modelBuilder.Entity<Fundort>(entity =>
            {
                entity.HasIndex(e => e.ArbeitszeiterfassungId, "IX_Fundort_ArbeitszeiterfassungId")
                      .IsUnique(true);
                entity.HasOne(f => f.ArbeitszeiterfassungNavigation)
                      .WithOne(aze => aze.FundortNavigation!)
                      .HasForeignKey<Fundort>(f => f.ArbeitszeiterfassungId);
            });

            modelBuilder.Entity<Stadt>(entity =>
            {

                entity.HasMany(e => e.Hausanschrifts)
                    .WithOne(ha => ha.StadtNavigation!)
                    .HasForeignKey(ha => ha.StadtId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stadt_HausanschriftId");

            });

            modelBuilder.Entity<Arbeitsandauer>().HasQueryFilter(e => e.AngestellteNavigation!.IstAngestellt);
            modelBuilder.Entity<Arbeitszeiterfassung>().HasQueryFilter(e => e.AngestellteNavigation!.IstAngestellt);
            modelBuilder.Entity<Hausanschrift>().HasQueryFilter(e => e.AngestellteNavigation!.IstAngestellt);
            modelBuilder.Entity<Kennwort>().HasQueryFilter(e => e.AngestellteNavigation!.IstAngestellt);
            
            /*
            modelBuilder.Entity<CustomerOrderViewModel>().HasNoKey().ToView("CustomerOrderView", "dbo");
            */
            /*
            modelBuilder.Entity<CreditRisk>(entity =>
            {
                entity.HasOne(d => d.CustomerNavigation)
                    .WithMany(p => p!.CreditRisks)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_CreditRisks_Customers");
                entity.OwnsOne(o => o.PersonalInformation,
                    pd =>
                    {
                        pd.Property<string>(nameof(Person.FirstName))
                            .HasColumnName(nameof(Person.FirstName))
                            .HasColumnType("nvarchar(50)");
                        pd.Property<string>(nameof(Person.LastName))
                            .HasColumnName(nameof(Person.LastName))
                            .HasColumnType("nvarchar(50)");
                        pd.Property(p => p.FullName)
                            .HasColumnName(nameof(Person.FullName))
                            .HasComputedColumnSql("[LastName] + ', ' + [FirstName]");
                    });
            });
            */
            /*
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.OwnsOne(o => o.PersonalInformation,
                    pd =>
                    {
                        pd.Property(p => p.FirstName).HasColumnName(nameof(Person.FirstName));
                        pd.Property(p => p.LastName).HasColumnName(nameof(Person.LastName));
                        pd.Property(p => p.FullName)
                            .HasColumnName(nameof(Person.FullName))
                            .HasComputedColumnSql("[LastName] + ', ' + [FirstName]");
                    });
                //entity.Navigation(c => c.PersonalInformation).IsRequired(true);
            });
            */
            /*
            modelBuilder.Entity<Make>(entity =>
            {
                entity.HasMany(e => e.Cars)
                    .WithOne(c => c.MakeNavigation!)
                    .HasForeignKey(k => k.MakeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Make_Inventory");
            });
            */
            /*
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasOne(d => d.CarNavigation)
                    .WithMany(p => p!.Orders)
                    .HasForeignKey(d => d.CarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Inventory");

                entity.HasOne(d => d.CustomerNavigation)
                    .WithMany(p => p!.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Orders_Customers");
                //entity.HasIndex(cr => new {cr.CustomerId, cr.CarId}).IsUnique(true);
            });
            */
            //New in EF Core 5 - bi-directional query filters
            //modelBuilder.Entity<Order>().HasQueryFilter(e => e.CarNavigation!.IsDrivable);
            OnModelCreatingPartial(modelBuilder);
        }
            
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Ein Parallelitätsfehler ist aufgetreten (A concurrency error occurred)
                // Sollte intelligent protokollieren und handhaben (Should log and handle intelligently )
                throw new CustomConcurrencyException("A concurrency error happened.", ex);
            }
            catch (RetryLimitExceededException ex)
            {
                //Db-Ausfallsicherheits-Wiederholungslimit überschritten (DbResiliency retry limit exceeded )
                // Sollte intelligent protokollieren und handhaben (Should log and handle intelligently )
                throw new CustomRetryLimitExceededException("There is a problem with SQl Server.", ex);
            }
            catch (DbUpdateException ex)
            {
                // Sollte intelligent protokollieren und handhaben (Should log and handle intelligently)
                throw new CustomDbUpdateException("An error occurred updating the database", ex);
            }
            catch (Exception ex)
            {
                // Sollte intelligent protokollieren und handhaben (Should log and handle intelligently)
                throw new CustomException("An error occurred updating the database", ex);
            }
            
        }
        
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Ein Parallelitätsfehler ist aufgetreten (A concurrency error occurred)
                // Sollte intelligent protokollieren und handhaben (Should log and handle intelligently )
                throw new CustomConcurrencyException("A concurrency error happened.", ex);
            }
            catch (RetryLimitExceededException ex)
            {
                //Db-Ausfallsicherheits-Wiederholungslimit überschritten (DbResiliency retry limit exceeded )
                // Sollte intelligent protokollieren und handhaben (Should log and handle intelligently )
                throw new CustomRetryLimitExceededException("There is a problem with SQl Server.", ex);
            }
            catch (DbUpdateException ex)
            {
                // Sollte intelligent protokollieren und handhaben (Should log and handle intelligently)
                throw new CustomDbUpdateException("An error occurred updating the database", ex);
            }
            catch (Exception ex)
            {
                // Sollte intelligent protokollieren und handhaben (Should log and handle intelligently)
                throw new CustomException("An error occurred updating the database", ex);
            }
        }
        
    }
}

