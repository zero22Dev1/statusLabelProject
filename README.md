BEGIN
  -- 外側の処理
  DBMS_OUTPUT.PUT_LINE('外側処理開始');

  BEGIN
    -- 内側の処理1（エラー発生）
    DBMS_OUTPUT.PUT_LINE('内側処理1 開始');
    DECLARE
      v1 NUMBER := 10 / 0;  -- ZERO_DIVIDE 発生
    BEGIN
      NULL;  -- 通常処理
    END;
  EXCEPTION
    WHEN ZERO_DIVIDE THEN
      DBMS_OUTPUT.PUT_LINE('内側処理1でゼロ除算エラー');
  END;

  BEGIN
    -- 内側の処理2（別のエラー）
    DBMS_OUTPUT.PUT_LINE('内側処理2 開始');
    DECLARE
      v2 NUMBER;
    BEGIN
      v2 := TO_NUMBER('abc');  -- VALUE_ERROR 発生
    END;
  EXCEPTION
    WHEN VALUE_ERROR THEN
      DBMS_OUTPUT.PUT_LINE('内側処理2で値変換エラー');
  END;

  DBMS_OUTPUT.PUT_LINE('外側処理終了');

EXCEPTION
  WHEN OTHERS THEN
    DBMS_OUTPUT.PUT_LINE('外側で未処理のエラー: ' || SQLERRM);
END;