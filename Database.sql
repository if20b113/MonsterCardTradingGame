--
-- PostgreSQL database dump
--

-- Dumped from database version 15.1 (Debian 15.1-1.pgdg110+1)
-- Dumped by pg_dump version 15.1

-- Started on 2023-01-15 21:42:57

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

--
-- TOC entry 3387 (class 1262 OID 16389)
-- Name: mctg; Type: DATABASE; Schema: -; Owner: swe1user
--

CREATE DATABASE mctg WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'en_US.utf8';


ALTER DATABASE mctg OWNER TO swe1user;

\connect mctg

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
-- TOC entry 216 (class 1259 OID 16425)
-- Name: authtokens; Type: TABLE; Schema: public; Owner: swe1user
--

CREATE TABLE public.authtokens (
    username character varying(256) NOT NULL,
    token character varying(256) NOT NULL,
    "timestamp" character varying(256) NOT NULL
);


ALTER TABLE public.authtokens OWNER TO swe1user;

--
-- TOC entry 217 (class 1259 OID 24617)
-- Name: cards; Type: TABLE; Schema: public; Owner: swe1user
--

CREATE TABLE public.cards (
    card_id character varying(256) NOT NULL,
    cardname character varying(256) NOT NULL,
    carddamage numeric NOT NULL,
    cardowner character varying(256),
    locked boolean DEFAULT false NOT NULL
);


ALTER TABLE public.cards OWNER TO swe1user;

--
-- TOC entry 221 (class 1259 OID 24641)
-- Name: decks; Type: TABLE; Schema: public; Owner: swe1user
--

CREATE TABLE public.decks (
    deck_id integer NOT NULL,
    deck_cards text[] NOT NULL,
    deck_owner character varying(256) NOT NULL,
    active boolean NOT NULL
);


ALTER TABLE public.decks OWNER TO swe1user;

--
-- TOC entry 220 (class 1259 OID 24640)
-- Name: decks_deck_id_seq; Type: SEQUENCE; Schema: public; Owner: swe1user
--

CREATE SEQUENCE public.decks_deck_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.decks_deck_id_seq OWNER TO swe1user;

--
-- TOC entry 3388 (class 0 OID 0)
-- Dependencies: 220
-- Name: decks_deck_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: swe1user
--

ALTER SEQUENCE public.decks_deck_id_seq OWNED BY public.decks.deck_id;


--
-- TOC entry 219 (class 1259 OID 24632)
-- Name: packages; Type: TABLE; Schema: public; Owner: swe1user
--

CREATE TABLE public.packages (
    p_id integer NOT NULL,
    p_cards text[] NOT NULL
);


ALTER TABLE public.packages OWNER TO swe1user;

--
-- TOC entry 218 (class 1259 OID 24631)
-- Name: packages_p_id_seq; Type: SEQUENCE; Schema: public; Owner: swe1user
--

CREATE SEQUENCE public.packages_p_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.packages_p_id_seq OWNER TO swe1user;

--
-- TOC entry 3389 (class 0 OID 0)
-- Dependencies: 218
-- Name: packages_p_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: swe1user
--

ALTER SEQUENCE public.packages_p_id_seq OWNED BY public.packages.p_id;


--
-- TOC entry 214 (class 1259 OID 16390)
-- Name: players; Type: TABLE; Schema: public; Owner: swe1user
--

CREATE TABLE public.players (
    username character varying(256) NOT NULL,
    password character varying(256) NOT NULL,
    coins integer NOT NULL
);


ALTER TABLE public.players OWNER TO swe1user;

--
-- TOC entry 215 (class 1259 OID 16413)
-- Name: profiles; Type: TABLE; Schema: public; Owner: swe1user
--

CREATE TABLE public.profiles (
    name character varying(256) NOT NULL,
    bio character varying(256) NOT NULL,
    image character varying(256) NOT NULL,
    username character varying(256) NOT NULL
);


ALTER TABLE public.profiles OWNER TO swe1user;

--
-- TOC entry 222 (class 1259 OID 24650)
-- Name: stats; Type: TABLE; Schema: public; Owner: swe1user
--

CREATE TABLE public.stats (
    username character varying(256) NOT NULL,
    name character varying(256) NOT NULL,
    elo numeric NOT NULL,
    wins numeric NOT NULL,
    losses numeric NOT NULL,
    gamesplayed numeric NOT NULL
);


ALTER TABLE public.stats OWNER TO swe1user;

--
-- TOC entry 223 (class 1259 OID 24669)
-- Name: tradingdeals; Type: TABLE; Schema: public; Owner: swe1user
--

CREATE TABLE public.tradingdeals (
    t_id character varying(256) NOT NULL,
    t_owner character varying(256) NOT NULL,
    cardtotrade character varying(256) NOT NULL,
    type character varying(256) NOT NULL,
    mindamage numeric(256,0) NOT NULL
);


ALTER TABLE public.tradingdeals OWNER TO swe1user;

--
-- TOC entry 3207 (class 2604 OID 24644)
-- Name: decks deck_id; Type: DEFAULT; Schema: public; Owner: swe1user
--

ALTER TABLE ONLY public.decks ALTER COLUMN deck_id SET DEFAULT nextval('public.decks_deck_id_seq'::regclass);


--
-- TOC entry 3206 (class 2604 OID 24635)
-- Name: packages p_id; Type: DEFAULT; Schema: public; Owner: swe1user
--

ALTER TABLE ONLY public.packages ALTER COLUMN p_id SET DEFAULT nextval('public.packages_p_id_seq'::regclass);


--
-- TOC entry 3374 (class 0 OID 16425)
-- Dependencies: 216
-- Data for Name: authtokens; Type: TABLE DATA; Schema: public; Owner: swe1user
--



--
-- TOC entry 3375 (class 0 OID 24617)
-- Dependencies: 217
-- Data for Name: cards; Type: TABLE DATA; Schema: public; Owner: swe1user
--



--
-- TOC entry 3379 (class 0 OID 24641)
-- Dependencies: 221
-- Data for Name: decks; Type: TABLE DATA; Schema: public; Owner: swe1user
--



--
-- TOC entry 3377 (class 0 OID 24632)
-- Dependencies: 219
-- Data for Name: packages; Type: TABLE DATA; Schema: public; Owner: swe1user
--



--
-- TOC entry 3372 (class 0 OID 16390)
-- Dependencies: 214
-- Data for Name: players; Type: TABLE DATA; Schema: public; Owner: swe1user
--



--
-- TOC entry 3373 (class 0 OID 16413)
-- Dependencies: 215
-- Data for Name: profiles; Type: TABLE DATA; Schema: public; Owner: swe1user
--



--
-- TOC entry 3380 (class 0 OID 24650)
-- Dependencies: 222
-- Data for Name: stats; Type: TABLE DATA; Schema: public; Owner: swe1user
--



--
-- TOC entry 3381 (class 0 OID 24669)
-- Dependencies: 223
-- Data for Name: tradingdeals; Type: TABLE DATA; Schema: public; Owner: swe1user
--



--
-- TOC entry 3390 (class 0 OID 0)
-- Dependencies: 220
-- Name: decks_deck_id_seq; Type: SEQUENCE SET; Schema: public; Owner: swe1user
--

SELECT pg_catalog.setval('public.decks_deck_id_seq', 2, true);


--
-- TOC entry 3391 (class 0 OID 0)
-- Dependencies: 218
-- Name: packages_p_id_seq; Type: SEQUENCE SET; Schema: public; Owner: swe1user
--

SELECT pg_catalog.setval('public.packages_p_id_seq', 97, true);


--
-- TOC entry 3209 (class 2606 OID 16410)
-- Name: players Players_pkey; Type: CONSTRAINT; Schema: public; Owner: swe1user
--

ALTER TABLE ONLY public.players
    ADD CONSTRAINT "Players_pkey" PRIMARY KEY (username);


--
-- TOC entry 3213 (class 2606 OID 16431)
-- Name: authtokens authtokens_pkey; Type: CONSTRAINT; Schema: public; Owner: swe1user
--

ALTER TABLE ONLY public.authtokens
    ADD CONSTRAINT authtokens_pkey PRIMARY KEY (username);


--
-- TOC entry 3215 (class 2606 OID 24623)
-- Name: cards cards_pkey; Type: CONSTRAINT; Schema: public; Owner: swe1user
--

ALTER TABLE ONLY public.cards
    ADD CONSTRAINT cards_pkey PRIMARY KEY (card_id);


--
-- TOC entry 3219 (class 2606 OID 24648)
-- Name: decks decks_pkey; Type: CONSTRAINT; Schema: public; Owner: swe1user
--

ALTER TABLE ONLY public.decks
    ADD CONSTRAINT decks_pkey PRIMARY KEY (deck_id);


--
-- TOC entry 3217 (class 2606 OID 24639)
-- Name: packages packages_pkey; Type: CONSTRAINT; Schema: public; Owner: swe1user
--

ALTER TABLE ONLY public.packages
    ADD CONSTRAINT packages_pkey PRIMARY KEY (p_id);


--
-- TOC entry 3211 (class 2606 OID 16419)
-- Name: profiles profiles_pkey; Type: CONSTRAINT; Schema: public; Owner: swe1user
--

ALTER TABLE ONLY public.profiles
    ADD CONSTRAINT profiles_pkey PRIMARY KEY (username);


--
-- TOC entry 3221 (class 2606 OID 24656)
-- Name: stats stats_pkey; Type: CONSTRAINT; Schema: public; Owner: swe1user
--

ALTER TABLE ONLY public.stats
    ADD CONSTRAINT stats_pkey PRIMARY KEY (username);


--
-- TOC entry 3223 (class 2606 OID 24675)
-- Name: tradingdeals tradingdeals_pkey; Type: CONSTRAINT; Schema: public; Owner: swe1user
--

ALTER TABLE ONLY public.tradingdeals
    ADD CONSTRAINT tradingdeals_pkey PRIMARY KEY (t_id);


--
-- TOC entry 3224 (class 2606 OID 16420)
-- Name: profiles profiles_Username_fkey; Type: FK CONSTRAINT; Schema: public; Owner: swe1user
--

ALTER TABLE ONLY public.profiles
    ADD CONSTRAINT "profiles_Username_fkey" FOREIGN KEY (username) REFERENCES public.players(username) ON DELETE CASCADE NOT VALID;


--
-- TOC entry 3229 (class 2606 OID 24676)
-- Name: tradingdeals username; Type: FK CONSTRAINT; Schema: public; Owner: swe1user
--

ALTER TABLE ONLY public.tradingdeals
    ADD CONSTRAINT username FOREIGN KEY (t_owner) REFERENCES public.players(username) ON DELETE CASCADE NOT VALID;


--
-- TOC entry 3227 (class 2606 OID 24681)
-- Name: decks username; Type: FK CONSTRAINT; Schema: public; Owner: swe1user
--

ALTER TABLE ONLY public.decks
    ADD CONSTRAINT username FOREIGN KEY (deck_owner) REFERENCES public.players(username) ON DELETE CASCADE NOT VALID;


--
-- TOC entry 3226 (class 2606 OID 24686)
-- Name: cards username; Type: FK CONSTRAINT; Schema: public; Owner: swe1user
--

ALTER TABLE ONLY public.cards
    ADD CONSTRAINT username FOREIGN KEY (cardowner) REFERENCES public.players(username) ON DELETE CASCADE NOT VALID;


--
-- TOC entry 3225 (class 2606 OID 24691)
-- Name: authtokens username; Type: FK CONSTRAINT; Schema: public; Owner: swe1user
--

ALTER TABLE ONLY public.authtokens
    ADD CONSTRAINT username FOREIGN KEY (username) REFERENCES public.players(username) ON DELETE CASCADE NOT VALID;


--
-- TOC entry 3228 (class 2606 OID 24664)
-- Name: stats username_player; Type: FK CONSTRAINT; Schema: public; Owner: swe1user
--

ALTER TABLE ONLY public.stats
    ADD CONSTRAINT username_player FOREIGN KEY (username) REFERENCES public.players(username) ON DELETE CASCADE NOT VALID;


-- Completed on 2023-01-15 21:42:57

--
-- PostgreSQL database dump complete
--

