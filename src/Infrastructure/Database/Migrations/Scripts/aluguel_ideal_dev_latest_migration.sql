/* AluguelIdealDatabaseMigration migrating =================================== */

/* Beginning Transaction */
/* ExecuteSqlStatement DROP TABLE IF EXISTS advertisement */
DROP TABLE IF EXISTS advertisement
/* => 0,1778676s */

/* ExecuteSqlStatement DROP TABLE IF EXISTS contact */
DROP TABLE IF EXISTS contact
/* => 0,1449704s */

/* CreateTable advertisement */
CREATE TABLE "public"."advertisement" ("id" serial NOT NULL, "title" varchar(255) NOT NULL, "deleted_at" timestamp, CONSTRAINT "PK_advertisement" PRIMARY KEY ("id"));
/* => 0,1816594s */

/* CreateTable contact */
CREATE TABLE "public"."contact" ("id" serial NOT NULL, "name" varchar(255) NOT NULL, "email" varchar(255) NOT NULL, "phone" varchar(255) NOT NULL, "deleted_at" timestamp, CONSTRAINT "PK_contact" PRIMARY KEY ("id"));
/* => 0,1699807s */

INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Quas voluptatibus hic nemo iste ratione dolores.',NULL);
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Assumenda ad et excepturi est quibusdam ullam et.','2020-07-27T01:47:01');
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Ratione ducimus autem et quasi et est.',NULL);
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Maxime velit voluptas.',NULL);
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Et qui et aliquam est voluptatem ut recusandae consequuntur.',NULL);
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Quaerat aliquam sunt exercitationem.','2020-04-15T01:01:41');
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Provident sunt et corrupti veniam.',NULL);
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Rerum voluptatem eos.',NULL);
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Repellat et eveniet dignissimos optio officiis aut sit.',NULL);
INSERT INTO "public"."advertisement" ("title","deleted_at") VALUES ('Soluta et fugit hic ducimus qui eum quis.',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Elisa Reis','Luiza15@example.org','+556920352868',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('João Pedro Braga','Caio.Saraiva@example.org','+554115494072',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Valentina Carvalho','Lorenzo70@example.net','+552881760282',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Júlio César Albuquerque','Ofelia.Albuquerque@example.net','+557094460934',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Maitê Martins','Frederico.Reis@example.org','+554946586118','2020-06-26T00:02:21');
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Nataniel Carvalho','JoaoPedro36@example.org','+553576681452',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Karla Reis','Alessandra.Macedo@example.net','+554684811406',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Isadora Albuquerque','Danilo.Moraes76@example.net','+557441972767',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Bruna Santos','Natalia89@example.net','+555431718066',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Gabriel Silva','Matheus64@example.com','+555194825522',NULL);
/* -> 20 Insert operations completed in 00:00:02.8809360 taking an average of 00:00:00.1440468 */
/* Committing Transaction */
/* AluguelIdealDatabaseMigration migrated */
/* => 0,3038952s */

