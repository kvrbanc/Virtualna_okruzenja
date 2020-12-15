--
-- PostgreSQL database dump
--

-- Dumped from database version 12.3
-- Dumped by pg_dump version 12.3

-- Started on 2020-12-15 21:08:45

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

DROP DATABASE virokr;
--
-- TOC entry 2849 (class 1262 OID 93721)
-- Name: virokr; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE virokr WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'English_United States.1252' LC_CTYPE = 'English_United States.1252';


ALTER DATABASE virokr OWNER TO postgres;

\connect virokr

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 202 (class 1259 OID 93722)
-- Name: auth_role; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.auth_role (
    id integer NOT NULL,
    description character varying(255),
    label character varying(255)
);


ALTER TABLE public.auth_role OWNER TO postgres;

--
-- TOC entry 204 (class 1259 OID 93732)
-- Name: auth_user; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.auth_user (
    id integer NOT NULL,
    password character varying(255) NOT NULL,
    username character varying(255) NOT NULL
);


ALTER TABLE public.auth_user OWNER TO postgres;

--
-- TOC entry 203 (class 1259 OID 93730)
-- Name: auth_user_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.auth_user_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.auth_user_id_seq OWNER TO postgres;

--
-- TOC entry 2850 (class 0 OID 0)
-- Dependencies: 203
-- Name: auth_user_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.auth_user_id_seq OWNED BY public.auth_user.id;


--
-- TOC entry 205 (class 1259 OID 93741)
-- Name: auth_user_role; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.auth_user_role (
    user_id integer NOT NULL,
    role_id integer NOT NULL
);


ALTER TABLE public.auth_user_role OWNER TO postgres;

--
-- TOC entry 207 (class 1259 OID 93748)
-- Name: game_score; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.game_score (
    id integer NOT NULL,
    value integer NOT NULL,
    user_id integer NOT NULL
);


ALTER TABLE public.game_score OWNER TO postgres;

--
-- TOC entry 206 (class 1259 OID 93746)
-- Name: game_score_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.game_score_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.game_score_id_seq OWNER TO postgres;

--
-- TOC entry 2851 (class 0 OID 0)
-- Dependencies: 206
-- Name: game_score_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.game_score_id_seq OWNED BY public.game_score.id;


--
-- TOC entry 2703 (class 2604 OID 93735)
-- Name: auth_user id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.auth_user ALTER COLUMN id SET DEFAULT nextval('public.auth_user_id_seq'::regclass);


--
-- TOC entry 2704 (class 2604 OID 93751)
-- Name: game_score id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.game_score ALTER COLUMN id SET DEFAULT nextval('public.game_score_id_seq'::regclass);


--
-- TOC entry 2706 (class 2606 OID 93729)
-- Name: auth_role auth_role_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.auth_role
    ADD CONSTRAINT auth_role_pkey PRIMARY KEY (id);


--
-- TOC entry 2708 (class 2606 OID 93740)
-- Name: auth_user auth_user_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.auth_user
    ADD CONSTRAINT auth_user_pkey PRIMARY KEY (id);


--
-- TOC entry 2712 (class 2606 OID 93745)
-- Name: auth_user_role auth_user_role_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.auth_user_role
    ADD CONSTRAINT auth_user_role_pkey PRIMARY KEY (user_id, role_id);


--
-- TOC entry 2714 (class 2606 OID 93753)
-- Name: game_score game_score_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.game_score
    ADD CONSTRAINT game_score_pkey PRIMARY KEY (id);


--
-- TOC entry 2710 (class 2606 OID 93755)
-- Name: auth_user uk_t1iph3dfc25ukwcl9xemtnojn; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.auth_user
    ADD CONSTRAINT uk_t1iph3dfc25ukwcl9xemtnojn UNIQUE (username);


--
-- TOC entry 2715 (class 2606 OID 93756)
-- Name: auth_user_role fk3eldmba9luu9l0apl0791x8vd; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.auth_user_role
    ADD CONSTRAINT fk3eldmba9luu9l0apl0791x8vd FOREIGN KEY (role_id) REFERENCES public.auth_role(id);


--
-- TOC entry 2716 (class 2606 OID 93761)
-- Name: auth_user_role fkebutsbqm58ehnlffb299ng0ap; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.auth_user_role
    ADD CONSTRAINT fkebutsbqm58ehnlffb299ng0ap FOREIGN KEY (user_id) REFERENCES public.auth_user(id);


--
-- TOC entry 2717 (class 2606 OID 93766)
-- Name: game_score fkhmp80nlkr2we1sf7df3i5ovuj; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.game_score
    ADD CONSTRAINT fkhmp80nlkr2we1sf7df3i5ovuj FOREIGN KEY (user_id) REFERENCES public.auth_user(id);


-- Completed on 2020-12-15 21:08:46

--
-- PostgreSQL database dump complete
--

