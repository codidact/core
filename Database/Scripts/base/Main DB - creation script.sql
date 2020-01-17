-- Database generated with pgModeler (PostgreSQL Database Modeler).
-- pgModeler  version: 0.9.2
-- PostgreSQL version: 12.0
-- Project Site: pgmodeler.io
-- Model Author: ---


-- Database creation must be done outside a multicommand file.
-- These commands were put in this file only as a convenience.
-- -- object: codidact_core | type: DATABASE --
-- -- DROP DATABASE IF EXISTS codidact_core;
-- CREATE DATABASE codidact_core
-- 	ENCODING = 'UTF8'
-- 	LC_COLLATE = 'C';
-- -- ddl-end --
-- 

-- object: public."Question" | type: TABLE --
-- DROP TABLE IF EXISTS public."Question" CASCADE;
CREATE TABLE public."Question"
(
	"Id" integer NOT NULL GENERATED ALWAYS AS IDENTITY,
	"CommunityId" integer,
	"DatetimeCreated" timestamptz NOT NULL DEFAULT NOW(),
	"Title" varchar(255) NOT NULL,
	"Body" varchar NOT NULL,
	CONSTRAINT "Question_pk" PRIMARY KEY ("Id")

);
-- ddl-end --
-- ALTER TABLE public."Question" OWNER TO postgres;
-- ddl-end --

-- object: public."Community" | type: TABLE --
-- DROP TABLE IF EXISTS public."Community" CASCADE;
CREATE TABLE public."Community"
(
	"Id" integer NOT NULL GENERATED ALWAYS AS IDENTITY,
	"Name" varchar(50) NOT NULL,
	CONSTRAINT "Community_pk" PRIMARY KEY ("Id")

);
-- ddl-end --
-- ALTER TABLE public."Community" OWNER TO postgres;
-- ddl-end --

-- object: "Community_fk" | type: CONSTRAINT --
-- ALTER TABLE public."Question" DROP CONSTRAINT IF EXISTS "Community_fk" CASCADE;
ALTER TABLE public."Question" ADD CONSTRAINT "Community_fk" FOREIGN KEY ("CommunityId")
REFERENCES public."Community" ("Id") MATCH FULL
ON DELETE SET NULL ON UPDATE CASCADE;
-- ddl-end --

-- object: public."Tag" | type: TABLE --
-- DROP TABLE IF EXISTS public."Tag" CASCADE;
CREATE TABLE public."Tag"
(
	"Id" integer NOT NULL GENERATED ALWAYS AS IDENTITY,
	"CommunityId" integer,
	"DatetimeCreated" timestamptz NOT NULL DEFAULT NOW(),
	"Title" varchar(40) NOT NULL,
	"Description" varchar,
	CONSTRAINT "Tag_pk" PRIMARY KEY ("Id")

);
-- ddl-end --
-- ALTER TABLE public."Tag" OWNER TO postgres;
-- ddl-end --

-- object: public."Question_Tag" | type: TABLE --
-- DROP TABLE IF EXISTS public."Question_Tag" CASCADE;
CREATE TABLE public."Question_Tag"
(
	"QuestionId" integer NOT NULL,
	"TagId" integer NOT NULL,
	CONSTRAINT "Question_Tag_pk" PRIMARY KEY ("QuestionId","TagId")

);
-- ddl-end --

-- object: "Question_fk" | type: CONSTRAINT --
-- ALTER TABLE public."Question_Tag" DROP CONSTRAINT IF EXISTS "Question_fk" CASCADE;
ALTER TABLE public."Question_Tag" ADD CONSTRAINT "Question_fk" FOREIGN KEY ("QuestionId")
REFERENCES public."Question" ("Id") MATCH FULL
ON DELETE RESTRICT ON UPDATE CASCADE;
-- ddl-end --

-- object: "Tag_fk" | type: CONSTRAINT --
-- ALTER TABLE public."Question_Tag" DROP CONSTRAINT IF EXISTS "Tag_fk" CASCADE;
ALTER TABLE public."Question_Tag" ADD CONSTRAINT "Tag_fk" FOREIGN KEY ("TagId")
REFERENCES public."Tag" ("Id") MATCH FULL
ON DELETE RESTRICT ON UPDATE CASCADE;
-- ddl-end --

-- object: "Community_fk" | type: CONSTRAINT --
-- ALTER TABLE public."Tag" DROP CONSTRAINT IF EXISTS "Community_fk" CASCADE;
ALTER TABLE public."Tag" ADD CONSTRAINT "Community_fk" FOREIGN KEY ("CommunityId")
REFERENCES public."Community" ("Id") MATCH FULL
ON DELETE SET NULL ON UPDATE CASCADE;
-- ddl-end --

-- object: "Tag_Title_uq" | type: CONSTRAINT --
-- ALTER TABLE public."Tag" DROP CONSTRAINT IF EXISTS "Tag_Title_uq" CASCADE;
ALTER TABLE public."Tag" ADD CONSTRAINT "Tag_Title_uq" UNIQUE ("CommunityId","Title");
-- ddl-end --


