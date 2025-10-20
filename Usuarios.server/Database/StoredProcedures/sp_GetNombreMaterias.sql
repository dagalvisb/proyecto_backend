CREATE OR REPLACE PROCEDURE sp_GetNombreMaterias(INOUT ref refcursor)
LANGUAGE plpgsql
AS $$
BEGIN
    OPEN ref FOR
        SELECT DISTINCT materia
        FROM incmaterias
        ORDER BY materia;
END;
$$;