SET statement_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = off;
SET check_function_bodies = false;
SET client_min_messages = warning;
SET escape_string_warning = off;

CREATE ROLE zbox LOGIN
  ENCRYPTED PASSWORD '....'
  NOSUPERUSER INHERIT CREATEDB NOCREATEROLE;

DROP DATABASE zbox CASCADE;
DROP DATABASE zbox_test CASCADE;

CREATE DATABASE zbox WITH TEMPLATE = template0 ENCODING = 'UTF8';
ALTER DATABASE zbox OWNER TO zbox;

CREATE DATABASE zbox_test WITH TEMPLATE = template0 ENCODING = 'UTF8';
ALTER DATABASE zbox_test OWNER TO zbox;

\connect zbox

SET statement_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = off;
SET check_function_bodies = false;
SET client_min_messages = warning;
SET escape_string_warning = off;

CREATE SCHEMA dbo;
ALTER SCHEMA dbo OWNER TO zbox;

CREATE PROCEDURAL LANGUAGE plpgsql;
ALTER PROCEDURAL LANGUAGE plpgsql OWNER TO postgres;

REVOKE ALL ON SCHEMA dbo FROM PUBLIC;
REVOKE ALL ON SCHEMA dbo FROM zbox;
GRANT ALL ON SCHEMA dbo TO zbox;

CREATE OR REPLACE FUNCTION public.uuid_generate_v4()
RETURNS uuid
AS '$libdir/uuid-ossp', 'uuid_generate_v4'
VOLATILE STRICT LANGUAGE C;

\connect zbox_test

SET statement_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = off;
SET check_function_bodies = false;
SET client_min_messages = warning;
SET escape_string_warning = off;

CREATE SCHEMA dbo;
ALTER SCHEMA dbo OWNER TO zbox;

CREATE PROCEDURAL LANGUAGE plpgsql;
ALTER PROCEDURAL LANGUAGE plpgsql OWNER TO postgres;

REVOKE ALL ON SCHEMA dbo FROM PUBLIC;
REVOKE ALL ON SCHEMA dbo FROM zbox;
GRANT ALL ON SCHEMA dbo TO zbox;

CREATE OR REPLACE FUNCTION public.uuid_generate_v4()
RETURNS uuid
AS '$libdir/uuid-ossp', 'uuid_generate_v4'
VOLATILE STRICT LANGUAGE C;
