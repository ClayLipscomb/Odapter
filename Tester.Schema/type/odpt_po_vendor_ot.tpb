CREATE OR REPLACE TYPE BODY ODPT.odpt_po_vendor_ot AS

   CONSTRUCTOR FUNCTION odpt_po_vendor_ot (
      vendor_id                        NUMBER,
      last_update_date                 DATE,
      last_updated_by                  NUMBER,
      vendor_name                      VARCHAR2,
      segment1                         VARCHAR2,
      summary_flag                     VARCHAR2,
      enabled_flag                     VARCHAR2,
      segment2                         VARCHAR2,
      segment3                         VARCHAR2,
      segment4                         VARCHAR2,
      segment5                         VARCHAR2,
      last_update_login                NUMBER,
      creation_date                    DATE,
      created_by                       NUMBER,
      employee_id                      NUMBER,
      validation_number                NUMBER,
      vendor_type_lookup_code          VARCHAR2,
      customer_num                     VARCHAR2,
      one_time_flag                    VARCHAR2,
      parent_vendor_id                 NUMBER,
      min_order_amount                 NUMBER,
      ship_to_location_id              NUMBER,
      bill_to_location_id              NUMBER,
      ship_via_lookup_code             VARCHAR2,
      freight_terms_lookup_code        VARCHAR2,
      fob_lookup_code                  VARCHAR2,
      terms_id                         NUMBER,
      set_of_books_id                  NUMBER,
      credit_status_lookup_code        VARCHAR2,
      credit_limit                     NUMBER,
      always_take_disc_flag            VARCHAR2,
      pay_date_basis_lookup_code       VARCHAR2,
      pay_group_lookup_code            VARCHAR2,
      payment_priority                 NUMBER,
      invoice_currency_code            VARCHAR2,
      payment_currency_code            VARCHAR2,
      invoice_amount_limit             NUMBER,
      exchange_date_lookup_code        VARCHAR2,
      hold_all_payments_flag           VARCHAR2,
      hold_future_payments_flag        VARCHAR2,
      hold_reason                      VARCHAR2,
      distribution_set_id              NUMBER,
      accts_pay_code_combination_id    NUMBER,
      disc_lost_code_combination_id    NUMBER,
      disc_taken_code_combination_id   NUMBER,
      expense_code_combination_id      NUMBER,
      prepay_code_combination_id       NUMBER,
      num_1099                         VARCHAR2,
      type_1099                        VARCHAR2,
      withholding_status_lookup_code   VARCHAR2,
      withholding_start_date           DATE,
      organization_type_lookup_code    VARCHAR2,
      vat_code                         VARCHAR2,
      start_date_active                DATE,
      end_date_active                  DATE,
      qty_rcv_tolerance                NUMBER,
      over_tolerance_error_flag        VARCHAR2,
      minority_group_lookup_code       VARCHAR2,
      payment_method_lookup_code       VARCHAR2,
      bank_account_name                VARCHAR2,
      bank_account_num                 VARCHAR2,
      bank_num                         VARCHAR2,
      bank_account_type                VARCHAR2,
      women_owned_flag                 VARCHAR2,
      small_business_flag              VARCHAR2,
      standard_industry_class          VARCHAR2,
      attribute_category               VARCHAR2,
      attribute1                       VARCHAR2,
      attribute2                       VARCHAR2,
      attribute3                       VARCHAR2,
      attribute4                       VARCHAR2,
      attribute5                       VARCHAR,
      hold_flag                        VARCHAR,
      purchasing_hold_reason           VARCHAR2,
      hold_by                          NUMBER,
      hold_date                        DATE,
      terms_date_basis                 VARCHAR2,
      price_tolerance                  NUMBER,
      inspection_required_flag         VARCHAR2,
      receipt_required_flag            VARCHAR2,
      qty_rcv_exception_code           VARCHAR2,
      enforce_ship_to_location_code    VARCHAR2,
      days_early_receipt_allowed       NUMBER,
      days_late_receipt_allowed        NUMBER,
      receipt_days_exception_code      VARCHAR2,
      receiving_routing_id             NUMBER,
      allow_substitute_receipts_flag   VARCHAR2,
      allow_unordered_receipts_flag    VARCHAR2,
      hold_unmatched_invoices_flag     VARCHAR2,
      exclusive_payment_flag           VARCHAR2,
      tax_verification_date            DATE,
      name_control                     VARCHAR2,
      state_reportable_flag            VARCHAR2,
      federal_reportable_flag          VARCHAR2,
      attribute6                       VARCHAR2,
      attribute7                       VARCHAR2,
      attribute8                       VARCHAR2,
      attribute9                       VARCHAR2,
      attribute10                      VARCHAR2,
      attribute11                      VARCHAR2,
      attribute12                      VARCHAR2,
      attribute13                      VARCHAR2,
      attribute14                      VARCHAR2,
      attribute15                      VARCHAR2,
      request_id                       NUMBER,
      program_application_id           NUMBER,
      program_id                       NUMBER,
      program_update_date              DATE,
      offset_vat_code                  VARCHAR2,
      vat_registration_num             VARCHAR2,
      auto_calculate_interest_flag     VARCHAR2,
      exclude_freight_from_discount    VARCHAR2,
      tax_reporting_name               VARCHAR2,
      check_digits                     VARCHAR2,
      bank_number                      VARCHAR2,
      allow_awt_flag                   VARCHAR2,
      awt_group_id                     NUMBER,
      vendor_name_alt                  VARCHAR2,
      ap_tax_rounding_rule             VARCHAR2,
      auto_tax_calc_flag               VARCHAR2,
      auto_tax_calc_override           VARCHAR2,
      amount_includes_tax_flag         VARCHAR2,
      global_attribute1                VARCHAR2,
      global_attribute2                VARCHAR2,
      global_attribute3                VARCHAR2,
      global_attribute4                VARCHAR2,
      global_attribute5                VARCHAR2,
      global_attribute6                VARCHAR2,
      global_attribute7                VARCHAR2,
      global_attribute8                VARCHAR2,
      global_attribute9                VARCHAR2,
      global_attribute10               VARCHAR2,
      global_attribute11               VARCHAR2,
      global_attribute12               VARCHAR2,
      global_attribute13               VARCHAR2,
      global_attribute14               VARCHAR2,
      global_attribute15               VARCHAR2,
      global_attribute16               VARCHAR2,
      global_attribute17               VARCHAR2,
      global_attribute18               VARCHAR2,
      global_attribute19               VARCHAR2,
      global_attribute20               VARCHAR2,
      global_attribute_category        VARCHAR2,
      edi_transaction_handling         VARCHAR2,
      edi_payment_method               VARCHAR2,
      edi_payment_format               VARCHAR2,
      edi_remittance_method            VARCHAR2,
      edi_remittance_instruction       VARCHAR2,
      bank_charge_bearer               VARCHAR2,
      bank_branch_type                 VARCHAR2,
      match_option                     VARCHAR2,
      future_dated_payment_ccid        NUMBER,
      create_debit_memo_flag           VARCHAR2,
      offset_tax_flag                  VARCHAR2,
      unique_tax_reference_num         NUMBER,
      partnership_utr                  NUMBER,
      partnership_name                 VARCHAR2,
      cis_enabled_flag                 VARCHAR2,
      first_name                       VARCHAR2,
      second_name                      VARCHAR2,
      last_name                        VARCHAR2,
      salutation                       VARCHAR2,
      trading_name                     VARCHAR2,
      work_reference                   VARCHAR2,
      company_registration_number      VARCHAR2,
      national_insurance_number        VARCHAR2,
      verification_number              VARCHAR2,
      verification_request_id          NUMBER,
      match_status_flag                VARCHAR2,
      cis_verification_date            DATE,
      individual_1099                  VARCHAR2,
      cis_parent_vendor_id             NUMBER,
      bus_class_last_certified_date    DATE,
      bus_class_last_certified_by      NUMBER,
      
      flexible_column1                 VARCHAR2,
      flexible_column2                 VARCHAR2,
      flexible_column3                 VARCHAR2,
      flexible_column4                 VARCHAR2,
      flexible_column5                 VARCHAR2,
      flexible_column6                 VARCHAR2,
      flexible_column7                 VARCHAR2,
      flexible_column8                 VARCHAR2,
      flexible_column9                 VARCHAR2,
      flexible_column10                VARCHAR2,
      flexible_column11                VARCHAR2,
      flexible_column12                VARCHAR2,
      flexible_column13                VARCHAR2,
      flexible_column14                VARCHAR2,
      flexible_column15                NVARCHAR2,
      flexible_column16                CHAR,
      flexible_column17                CLOB,
      flexible_column18                DATE,   
      flexible_column19                DATE,
      flexible_column20                TIMESTAMP,   
      flexible_column21                NUMBER,
      flexible_column22                NUMBER,
      flexible_column23                NUMBER,
      flexible_column24                NUMBER,
      flexible_column25                NUMBER
   ) RETURN SELF AS RESULT IS
   BEGIN
      SELF.vendor_id                        := vendor_id                        ;
      SELF.last_update_date                 := last_update_date                 ;           
      SELF.last_updated_by                  := last_updated_by                  ;
      SELF.vendor_name                      := vendor_name                      ; 
      SELF.segment1                         := segment1                         ;    
      SELF.summary_flag                     := summary_flag                     ;    
      SELF.enabled_flag                     := enabled_flag                     ;    
      SELF.segment2                         := segment2                         ;        
      SELF.segment3                         := segment3                         ;        
      SELF.segment4                         := segment4                         ;        
      SELF.segment5                         := segment5                         ;        
      SELF.last_update_login                := last_update_login                ;
      SELF.creation_date                    := creation_date                    ;
      SELF.created_by                       := created_by                       ;
      SELF.employee_id                      := employee_id                      ;
      SELF.validation_number                := validation_number                ;
      SELF.vendor_type_lookup_code          := vendor_type_lookup_code          ;
      SELF.customer_num                     := customer_num                     ;
      SELF.one_time_flag                    := one_time_flag                    ;
      SELF.parent_vendor_id                 := parent_vendor_id                 ;
      SELF.min_order_amount                 := min_order_amount                 ;
      SELF.ship_to_location_id              := ship_to_location_id              ;
      SELF.bill_to_location_id              := bill_to_location_id              ;
      SELF.ship_via_lookup_code             := ship_via_lookup_code             ;
      SELF.freight_terms_lookup_code        := freight_terms_lookup_code        ;
      SELF.fob_lookup_code                  := fob_lookup_code                  ;
      SELF.terms_id                         := terms_id                         ;
      SELF.set_of_books_id                  := set_of_books_id                  ;
      SELF.credit_status_lookup_code        := credit_status_lookup_code        ;
      SELF.credit_limit                     := credit_limit                     ;
      SELF.always_take_disc_flag            := always_take_disc_flag            ;
      SELF.pay_date_basis_lookup_code       := pay_date_basis_lookup_code       ;
      SELF.pay_group_lookup_code            := pay_group_lookup_code            ;
      SELF.payment_priority                 := payment_priority                 ;
      SELF.invoice_currency_code            := invoice_currency_code            ;
      SELF.payment_currency_code            := payment_currency_code            ;
      SELF.invoice_amount_limit             := invoice_amount_limit             ;
      SELF.exchange_date_lookup_code        := exchange_date_lookup_code        ;
      SELF.hold_all_payments_flag           := hold_all_payments_flag           ;
      SELF.hold_future_payments_flag        := hold_future_payments_flag        ;
      SELF.hold_reason                      := hold_reason                      ;
      SELF.distribution_set_id              := distribution_set_id              ;
      SELF.accts_pay_code_combination_id    := accts_pay_code_combination_id    ;
      SELF.disc_lost_code_combination_id    := disc_lost_code_combination_id    ;
      SELF.disc_taken_code_combination_id   := disc_taken_code_combination_id   ;
      SELF.expense_code_combination_id      := expense_code_combination_id      ;
      SELF.prepay_code_combination_id       := prepay_code_combination_id       ;
      SELF.num_1099                         := num_1099                         ;
      SELF.type_1099                        := type_1099                        ;
      SELF.withholding_status_lookup_code   := withholding_status_lookup_code   ;
      SELF.withholding_start_date           := withholding_start_date           ;
      SELF.organization_type_lookup_code    := organization_type_lookup_code    ;
      SELF.vat_code                         := vat_code                         ;
      SELF.start_date_active                := start_date_active                ;
      SELF.end_date_active                  := end_date_active                  ;
      SELF.qty_rcv_tolerance                := qty_rcv_tolerance                ;
      SELF.over_tolerance_error_flag        := over_tolerance_error_flag        ;
      SELF.minority_group_lookup_code       := minority_group_lookup_code       ;
      SELF.payment_method_lookup_code       := payment_method_lookup_code       ;
      SELF.bank_account_name                := bank_account_name                ;
      SELF.bank_account_num                 := bank_account_num                 ;
      SELF.bank_num                         := bank_num                         ;
      SELF.bank_account_type                := bank_account_type                ;
      SELF.women_owned_flag                 := women_owned_flag                 ;
      SELF.small_business_flag              := small_business_flag              ;
      SELF.standard_industry_class          := standard_industry_class          ;
      SELF.attribute_category               := attribute_category               ;
      SELF.attribute1                       := attribute1                       ;
      SELF.attribute2                       := attribute2                       ;
      SELF.attribute3                       := attribute3                       ;
      SELF.attribute4                       := attribute4                       ;
      SELF.attribute5                       := attribute5                       ;
      SELF.hold_flag                        := hold_flag                        ;
      SELF.purchasing_hold_reason           := purchasing_hold_reason           ;
      SELF.hold_by                          := hold_by                          ;
      SELF.hold_date                        := hold_date                        ;
      SELF.terms_date_basis                 := terms_date_basis                 ;
      SELF.price_tolerance                  := price_tolerance                  ;
      SELF.inspection_required_flag         := inspection_required_flag         ;
      SELF.receipt_required_flag            := receipt_required_flag            ;
      SELF.qty_rcv_exception_code           := qty_rcv_exception_code           ;
      SELF.enforce_ship_to_location_code    := enforce_ship_to_location_code    ;
      SELF.days_early_receipt_allowed       := days_early_receipt_allowed       ;
      SELF.days_late_receipt_allowed        := days_late_receipt_allowed        ;
      SELF.receipt_days_exception_code      := receipt_days_exception_code      ;
      SELF.receiving_routing_id             := receiving_routing_id             ;
      SELF.allow_substitute_receipts_flag   := allow_substitute_receipts_flag   ;
      SELF.allow_unordered_receipts_flag    := allow_unordered_receipts_flag    ;
      SELF.hold_unmatched_invoices_flag     := hold_unmatched_invoices_flag     ;
      SELF.exclusive_payment_flag           := exclusive_payment_flag           ;
      SELF.tax_verification_date            := tax_verification_date            ;
      SELF.name_control                     := name_control                     ;
      SELF.state_reportable_flag            := state_reportable_flag            ;
      SELF.federal_reportable_flag          := federal_reportable_flag          ;
      SELF.attribute6                       := attribute6                       ;
      SELF.attribute7                       := attribute7                       ;
      SELF.attribute8                       := attribute8                       ;
      SELF.attribute9                       := attribute9                       ;
      SELF.attribute10                      := attribute10                      ;
      SELF.attribute11                      := attribute11                      ;
      SELF.attribute12                      := attribute12                      ;
      SELF.attribute13                      := attribute13                      ;
      SELF.attribute14                      := attribute14                      ;
      SELF.attribute15                      := attribute15                      ;
      SELF.request_id                       := request_id                       ;
      SELF.program_application_id           := program_application_id           ;
      SELF.program_id                       := program_id                       ;
      SELF.program_update_date              := program_update_date              ;
      SELF.offset_vat_code                  := offset_vat_code                  ;
      SELF.vat_registration_num             := vat_registration_num             ;
      SELF.auto_calculate_interest_flag     := auto_calculate_interest_flag     ;
      SELF.exclude_freight_from_discount    := exclude_freight_from_discount    ;
      SELF.tax_reporting_name               := tax_reporting_name               ;
      SELF.check_digits                     := check_digits                     ;
      SELF.bank_number                      := bank_number                      ;
      SELF.allow_awt_flag                   := allow_awt_flag                   ;
      SELF.awt_group_id                     := awt_group_id                     ;
      SELF.vendor_name_alt                  := vendor_name_alt                  ;
      SELF.ap_tax_rounding_rule             := ap_tax_rounding_rule             ;
      SELF.auto_tax_calc_flag               := auto_tax_calc_flag               ;
      SELF.auto_tax_calc_override           := auto_tax_calc_override           ;
      SELF.amount_includes_tax_flag         := amount_includes_tax_flag         ;
      SELF.global_attribute1                := global_attribute1                ;
      SELF.global_attribute2                := global_attribute2                ;
      SELF.global_attribute3                := global_attribute3                ;
      SELF.global_attribute4                := global_attribute4                ;
      SELF.global_attribute5                := global_attribute5                ;
      SELF.global_attribute6                := global_attribute6                ;
      SELF.global_attribute7                := global_attribute7                ;
      SELF.global_attribute8                := global_attribute8                ;
      SELF.global_attribute9                := global_attribute9                ;
      SELF.global_attribute10               := global_attribute10               ;
      SELF.global_attribute11               := global_attribute11               ;
      SELF.global_attribute12               := global_attribute12               ;
      SELF.global_attribute13               := global_attribute13               ;
      SELF.global_attribute14               := global_attribute14               ;
      SELF.global_attribute15               := global_attribute15               ;
      SELF.global_attribute16               := global_attribute16               ;
      SELF.global_attribute17               := global_attribute17               ;
      SELF.global_attribute18               := global_attribute18               ;
      SELF.global_attribute19               := global_attribute19               ;
      SELF.global_attribute20               := global_attribute20               ;
      SELF.global_attribute_category        := global_attribute_category        ;
      SELF.edi_transaction_handling         := edi_transaction_handling         ;
      SELF.edi_payment_method               := edi_payment_method               ;
      SELF.edi_payment_format               := edi_payment_format               ;
      SELF.edi_remittance_method            := edi_remittance_method            ;
      SELF.edi_remittance_instruction       := edi_remittance_instruction       ;
      SELF.bank_charge_bearer               := bank_charge_bearer               ;
      SELF.bank_branch_type                 := bank_branch_type                 ;
      SELF.match_option                     := match_option                     ;
      SELF.future_dated_payment_ccid        := future_dated_payment_ccid        ;
      SELF.create_debit_memo_flag           := create_debit_memo_flag           ;
      SELF.offset_tax_flag                  := offset_tax_flag                  ;
      SELF.unique_tax_reference_num         := unique_tax_reference_num         ;
      SELF.partnership_utr                  := partnership_utr                  ;
      SELF.partnership_name                 := partnership_name                 ;
      SELF.cis_enabled_flag                 := cis_enabled_flag                 ;
      SELF.first_name                       := first_name                       ;
      SELF.second_name                      := second_name                      ;
      SELF.last_name                        := last_name                        ;
      SELF.salutation                       := salutation                       ;
      SELF.trading_name                     := trading_name                     ;
      SELF.work_reference                   := work_reference                   ;
      SELF.company_registration_number      := company_registration_number      ;
      SELF.national_insurance_number        := national_insurance_number        ;
      SELF.verification_number              := verification_number              ;
      SELF.verification_request_id          := verification_request_id          ;
      SELF.match_status_flag                := match_status_flag                ;
      SELF.cis_verification_date            := cis_verification_date            ;
      SELF.individual_1099                  := individual_1099                  ;
      SELF.cis_parent_vendor_id             := cis_parent_vendor_id             ;
      SELF.bus_class_last_certified_date    := bus_class_last_certified_date    ;
      SELF.bus_class_last_certified_by      := bus_class_last_certified_by      ;

      -- initalize flex columns defined on base type
      SELF.init_flexible_data(      
      flexible_column1,    flexible_column2,    flexible_column3,    flexible_column4,    flexible_column5  ,
      flexible_column6,    flexible_column7,    flexible_column8,    flexible_column9,    flexible_column10 ,
      flexible_column11,   flexible_column12,   flexible_column13,   flexible_column14,   flexible_column15 ,
      flexible_column16,   flexible_column17,   flexible_column18,   flexible_column19,   flexible_column20,
      flexible_column21,   flexible_column22,   flexible_column23,   flexible_column24,   flexible_column25 );

      RETURN;
   END odpt_po_vendor_ot;

   OVERRIDING MEMBER FUNCTION get_pk_id RETURN VARCHAR2 IS
   BEGIN
      RETURN SELF.create_pk_id(TO_CHAR(SELF.vendor_id));
   END get_pk_id;

   OVERRIDING MEMBER FUNCTION has_same_state(p_base_object odpt_base_sc_trans_type_ot) RETURN BOOLEAN IS
      l_trans_obj odpt_po_vendor_ot;
   BEGIN
      IF p_base_object IS NULL THEN
         RETURN FALSE;
      END IF;

      l_trans_obj := TREAT(p_base_object AS odpt_po_vendor_ot);
      RETURN  (
               --SELF.equal_number    (SELF.vendor_id                         , l_trans_obj.vendor_id                     )
               SELF.equal_date      (SELF.last_update_date                  , l_trans_obj.last_update_date              )
         AND   SELF.equal_number    (SELF.last_updated_by                   , l_trans_obj.last_updated_by               )
         AND   SELF.equal_varchar2  (SELF.vendor_name                       , l_trans_obj.vendor_name                   )
         AND   SELF.equal_varchar2  (SELF.segment1                          , l_trans_obj.segment1                      )
         AND   SELF.equal_varchar2  (SELF.summary_flag                      , l_trans_obj.summary_flag                  )
         AND   SELF.equal_varchar2  (SELF.enabled_flag                      , l_trans_obj.enabled_flag                  )
         AND   SELF.equal_varchar2  (SELF.segment2                          , l_trans_obj.segment2                      )
         AND   SELF.equal_varchar2  (SELF.segment3                          , l_trans_obj.segment3                      )
         AND   SELF.equal_varchar2  (SELF.segment4                          , l_trans_obj.segment4                      )
         AND   SELF.equal_varchar2  (SELF.segment5                          , l_trans_obj.segment5                      )
         AND   SELF.equal_number    (SELF.last_update_login                 , l_trans_obj.last_update_login             )
         AND   SELF.equal_varchar2  (SELF.creation_date                     , l_trans_obj.creation_date                 )
         AND   SELF.equal_number    (SELF.created_by                        , l_trans_obj.created_by                    )
         AND   SELF.equal_number    (SELF.employee_id                       , l_trans_obj.employee_id                   )
         AND   SELF.equal_number    (SELF.validation_number                 , l_trans_obj.validation_number             )
         AND   SELF.equal_varchar2  (SELF.vendor_type_lookup_code           , l_trans_obj.vendor_type_lookup_code       )
         AND   SELF.equal_varchar2  (SELF.customer_num                      , l_trans_obj.customer_num                  )
         AND   SELF.equal_varchar2  (SELF.one_time_flag                     , l_trans_obj.one_time_flag                 )
         AND   SELF.equal_number    (SELF.parent_vendor_id                  , l_trans_obj.parent_vendor_id              )
         AND   SELF.equal_number    (SELF.min_order_amount                  , l_trans_obj.min_order_amount              )
         AND   SELF.equal_number    (SELF.ship_to_location_id               , l_trans_obj.ship_to_location_id           )
         AND   SELF.equal_number    (SELF.bill_to_location_id               , l_trans_obj.bill_to_location_id           )
         AND   SELF.equal_varchar2  (SELF.ship_via_lookup_code              , l_trans_obj.ship_via_lookup_code          )
         AND   SELF.equal_varchar2  (SELF.freight_terms_lookup_code         , l_trans_obj.freight_terms_lookup_code     )
         AND   SELF.equal_varchar2  (SELF.fob_lookup_code                   , l_trans_obj.fob_lookup_code               )
         AND   SELF.equal_number    (SELF.terms_id                          , l_trans_obj.terms_id                      )
         AND   SELF.equal_number    (SELF.set_of_books_id                   , l_trans_obj.set_of_books_id               )
         AND   SELF.equal_varchar2  (SELF.credit_status_lookup_code         , l_trans_obj.credit_status_lookup_code     )
         AND   SELF.equal_number    (SELF.credit_limit                      , l_trans_obj.credit_limit                  )
         AND   SELF.equal_varchar2  (SELF.always_take_disc_flag             , l_trans_obj.always_take_disc_flag         )
         AND   SELF.equal_varchar2  (SELF.pay_date_basis_lookup_code        , l_trans_obj.pay_date_basis_lookup_code    )
         AND   SELF.equal_varchar2  (SELF.pay_group_lookup_code             , l_trans_obj.pay_group_lookup_code         )
         AND   SELF.equal_number    (SELF.payment_priority                  , l_trans_obj.payment_priority              )
         AND   SELF.equal_varchar2  (SELF.invoice_currency_code             , l_trans_obj.invoice_currency_code         )
         AND   SELF.equal_varchar2  (SELF.payment_currency_code             , l_trans_obj.payment_currency_code         )
         AND   SELF.equal_number    (SELF.invoice_amount_limit              , l_trans_obj.invoice_amount_limit          )
         AND   SELF.equal_varchar2  (SELF.exchange_date_lookup_code         , l_trans_obj.exchange_date_lookup_code     )
         AND   SELF.equal_varchar2  (SELF.hold_all_payments_flag            , l_trans_obj.hold_all_payments_flag        )
         AND   SELF.equal_varchar2  (SELF.hold_future_payments_flag         , l_trans_obj.hold_future_payments_flag     )
         AND   SELF.equal_varchar2  (SELF.hold_reason                       , l_trans_obj.hold_reason                   )
         AND   SELF.equal_number    (SELF.distribution_set_id               , l_trans_obj.distribution_set_id           )
         AND   SELF.equal_number    (SELF.accts_pay_code_combination_id     , l_trans_obj.accts_pay_code_combination_id )
         AND   SELF.equal_number    (SELF.disc_lost_code_combination_id     , l_trans_obj.disc_lost_code_combination_id )
         AND   SELF.equal_number    (SELF.disc_taken_code_combination_id    , l_trans_obj.disc_taken_code_combination_id)
         AND   SELF.equal_number    (SELF.expense_code_combination_id       , l_trans_obj.expense_code_combination_id   )
         AND   SELF.equal_number    (SELF.prepay_code_combination_id        , l_trans_obj.prepay_code_combination_id    )
         AND   SELF.equal_varchar2  (SELF.num_1099                          , l_trans_obj.num_1099                      )
         AND   SELF.equal_varchar2  (SELF.type_1099                         , l_trans_obj.type_1099                     )
         AND   SELF.equal_varchar2  (SELF.withholding_status_lookup_code    , l_trans_obj.withholding_status_lookup_code)
         AND   SELF.equal_varchar2  (SELF.withholding_start_date            , l_trans_obj.withholding_start_date        )
         AND   SELF.equal_varchar2  (SELF.organization_type_lookup_code     , l_trans_obj.organization_type_lookup_code )
         AND   SELF.equal_varchar2  (SELF.vat_code                          , l_trans_obj.vat_code                      )
         AND   SELF.equal_varchar2  (SELF.start_date_active                 , l_trans_obj.start_date_active             )
         AND   SELF.equal_varchar2  (SELF.end_date_active                   , l_trans_obj.end_date_active               )
         AND   SELF.equal_number    (SELF.qty_rcv_tolerance                 , l_trans_obj.qty_rcv_tolerance             )
         AND   SELF.equal_varchar2  (SELF.over_tolerance_error_flag         , l_trans_obj.over_tolerance_error_flag     )
         AND   SELF.equal_varchar2  (SELF.minority_group_lookup_code        , l_trans_obj.minority_group_lookup_code    )
         AND   SELF.equal_varchar2  (SELF.payment_method_lookup_code        , l_trans_obj.payment_method_lookup_code    )
         AND   SELF.equal_varchar2  (SELF.bank_account_name                 , l_trans_obj.bank_account_name             )
         AND   SELF.equal_varchar2  (SELF.bank_account_num                  , l_trans_obj.bank_account_num              )
         AND   SELF.equal_varchar2  (SELF.bank_num                          , l_trans_obj.bank_num                      )
         AND   SELF.equal_varchar2  (SELF.bank_account_type                 , l_trans_obj.bank_account_type             )
         AND   SELF.equal_varchar2  (SELF.women_owned_flag                  , l_trans_obj.women_owned_flag              )
         AND   SELF.equal_varchar2  (SELF.small_business_flag               , l_trans_obj.small_business_flag           )
         AND   SELF.equal_varchar2  (SELF.standard_industry_class           , l_trans_obj.standard_industry_class       )
         AND   SELF.equal_varchar2  (SELF.attribute_category                , l_trans_obj.attribute_category            )
         AND   SELF.equal_varchar2  (SELF.attribute1                        , l_trans_obj.attribute1                    )
         AND   SELF.equal_varchar2  (SELF.attribute2                        , l_trans_obj.attribute2                    )
         AND   SELF.equal_varchar2  (SELF.attribute3                        , l_trans_obj.attribute3                    )
         AND   SELF.equal_varchar2  (SELF.attribute4                        , l_trans_obj.attribute4                    )
         AND   SELF.equal_varchar2  (SELF.attribute5                        , l_trans_obj.attribute5                    )
         AND   SELF.equal_varchar2  (SELF.hold_flag                         , l_trans_obj.hold_flag                     )
         AND   SELF.equal_varchar2  (SELF.purchasing_hold_reason            , l_trans_obj.purchasing_hold_reason        )
         AND   SELF.equal_number    (SELF.hold_by                           , l_trans_obj.hold_by                       )
         AND   SELF.equal_varchar2  (SELF.hold_date                         , l_trans_obj.hold_date                     )
         AND   SELF.equal_varchar2  (SELF.terms_date_basis                  , l_trans_obj.terms_date_basis              )
         AND   SELF.equal_number    (SELF.price_tolerance                   , l_trans_obj.price_tolerance               )
         AND   SELF.equal_varchar2  (SELF.inspection_required_flag          , l_trans_obj.inspection_required_flag      )
         AND   SELF.equal_varchar2  (SELF.receipt_required_flag             , l_trans_obj.receipt_required_flag         )
         AND   SELF.equal_varchar2  (SELF.qty_rcv_exception_code            , l_trans_obj.qty_rcv_exception_code        )
         AND   SELF.equal_varchar2  (SELF.enforce_ship_to_location_code     , l_trans_obj.enforce_ship_to_location_code )
         AND   SELF.equal_number    (SELF.days_early_receipt_allowed        , l_trans_obj.days_early_receipt_allowed    )
         AND   SELF.equal_number    (SELF.days_late_receipt_allowed         , l_trans_obj.days_late_receipt_allowed     )
         AND   SELF.equal_varchar2  (SELF.receipt_days_exception_code       , l_trans_obj.receipt_days_exception_code   )
         AND   SELF.equal_number    (SELF.receiving_routing_id              , l_trans_obj.receiving_routing_id          )
         AND   SELF.equal_varchar2  (SELF.allow_substitute_receipts_flag    , l_trans_obj.allow_substitute_receipts_flag)
         AND   SELF.equal_varchar2  (SELF.allow_unordered_receipts_flag     , l_trans_obj.allow_unordered_receipts_flag )
         AND   SELF.equal_varchar2  (SELF.hold_unmatched_invoices_flag      , l_trans_obj.hold_unmatched_invoices_flag  )
         AND   SELF.equal_varchar2  (SELF.exclusive_payment_flag            , l_trans_obj.exclusive_payment_flag        )
         AND   SELF.equal_varchar2  (SELF.tax_verification_date             , l_trans_obj.tax_verification_date         )
         AND   SELF.equal_varchar2  (SELF.name_control                      , l_trans_obj.name_control                  )
         AND   SELF.equal_varchar2  (SELF.state_reportable_flag             , l_trans_obj.state_reportable_flag         )
         AND   SELF.equal_varchar2  (SELF.federal_reportable_flag           , l_trans_obj.federal_reportable_flag       )
         AND   SELF.equal_varchar2  (SELF.attribute6                        , l_trans_obj.attribute6                    )
         AND   SELF.equal_varchar2  (SELF.attribute7                        , l_trans_obj.attribute7                    )
         AND   SELF.equal_varchar2  (SELF.attribute8                        , l_trans_obj.attribute8                    )
         AND   SELF.equal_varchar2  (SELF.attribute9                        , l_trans_obj.attribute9                    )
         AND   SELF.equal_varchar2  (SELF.attribute10                       , l_trans_obj.attribute10                   )
         AND   SELF.equal_varchar2  (SELF.attribute11                       , l_trans_obj.attribute11                   )
         AND   SELF.equal_varchar2  (SELF.attribute12                       , l_trans_obj.attribute12                   )
         AND   SELF.equal_varchar2  (SELF.attribute13                       , l_trans_obj.attribute13                   )
         AND   SELF.equal_varchar2  (SELF.attribute14                       , l_trans_obj.attribute14                   )
         AND   SELF.equal_varchar2  (SELF.attribute15                       , l_trans_obj.attribute15                   )
         AND   SELF.equal_number    (SELF.request_id                        , l_trans_obj.request_id                    )
         AND   SELF.equal_number    (SELF.program_application_id            , l_trans_obj.program_application_id        )
         AND   SELF.equal_number    (SELF.program_id                        , l_trans_obj.program_id                    )
         AND   SELF.equal_varchar2  (SELF.program_update_date               , l_trans_obj.program_update_date           )
         AND   SELF.equal_varchar2  (SELF.offset_vat_code                   , l_trans_obj.offset_vat_code               )
         AND   SELF.equal_varchar2  (SELF.vat_registration_num              , l_trans_obj.vat_registration_num          )
         AND   SELF.equal_varchar2  (SELF.auto_calculate_interest_flag      , l_trans_obj.auto_calculate_interest_flag  )
         AND   SELF.equal_varchar2  (SELF.exclude_freight_from_discount     , l_trans_obj.exclude_freight_from_discount )
         AND   SELF.equal_varchar2  (SELF.tax_reporting_name                , l_trans_obj.tax_reporting_name            )
         AND   SELF.equal_varchar2  (SELF.check_digits                      , l_trans_obj.check_digits                  )
         AND   SELF.equal_varchar2  (SELF.bank_number                       , l_trans_obj.bank_number                   )
         AND   SELF.equal_varchar2  (SELF.allow_awt_flag                    , l_trans_obj.allow_awt_flag                )
         AND   SELF.equal_number    (SELF.awt_group_id                      , l_trans_obj.awt_group_id                  )
         AND   SELF.equal_varchar2  (SELF.vendor_name_alt                   , l_trans_obj.vendor_name_alt               )
         AND   SELF.equal_varchar2  (SELF.ap_tax_rounding_rule              , l_trans_obj.ap_tax_rounding_rule          )
         AND   SELF.equal_varchar2  (SELF.auto_tax_calc_flag                , l_trans_obj.auto_tax_calc_flag            )
         AND   SELF.equal_varchar2  (SELF.auto_tax_calc_override            , l_trans_obj.auto_tax_calc_override        )
         AND   SELF.equal_varchar2  (SELF.amount_includes_tax_flag          , l_trans_obj.amount_includes_tax_flag      )
         AND   SELF.equal_varchar2  (SELF.global_attribute1                 , l_trans_obj.global_attribute1             )
         AND   SELF.equal_varchar2  (SELF.global_attribute2                 , l_trans_obj.global_attribute2             )
         AND   SELF.equal_varchar2  (SELF.global_attribute3                 , l_trans_obj.global_attribute3             )
         AND   SELF.equal_varchar2  (SELF.global_attribute4                 , l_trans_obj.global_attribute4             )
         AND   SELF.equal_varchar2  (SELF.global_attribute5                 , l_trans_obj.global_attribute5             )
         AND   SELF.equal_varchar2  (SELF.global_attribute6                 , l_trans_obj.global_attribute6             )
         AND   SELF.equal_varchar2  (SELF.global_attribute7                 , l_trans_obj.global_attribute7             )
         AND   SELF.equal_varchar2  (SELF.global_attribute8                 , l_trans_obj.global_attribute8             )
         AND   SELF.equal_varchar2  (SELF.global_attribute9                 , l_trans_obj.global_attribute9             )
         AND   SELF.equal_varchar2  (SELF.global_attribute10                , l_trans_obj.global_attribute10            )
         AND   SELF.equal_varchar2  (SELF.global_attribute11                , l_trans_obj.global_attribute11            )
         AND   SELF.equal_varchar2  (SELF.global_attribute12                , l_trans_obj.global_attribute12            )
         AND   SELF.equal_varchar2  (SELF.global_attribute13                , l_trans_obj.global_attribute13            )
         AND   SELF.equal_varchar2  (SELF.global_attribute14                , l_trans_obj.global_attribute14            )
         AND   SELF.equal_varchar2  (SELF.global_attribute15                , l_trans_obj.global_attribute15            )
         AND   SELF.equal_varchar2  (SELF.global_attribute16                , l_trans_obj.global_attribute16            )
         AND   SELF.equal_varchar2  (SELF.global_attribute17                , l_trans_obj.global_attribute17            )
         AND   SELF.equal_varchar2  (SELF.global_attribute18                , l_trans_obj.global_attribute18            )
         AND   SELF.equal_varchar2  (SELF.global_attribute19                , l_trans_obj.global_attribute19            )
         AND   SELF.equal_varchar2  (SELF.global_attribute20                , l_trans_obj.global_attribute20            )
         AND   SELF.equal_varchar2  (SELF.global_attribute_category         , l_trans_obj.global_attribute_category     )
         AND   SELF.equal_varchar2  (SELF.edi_transaction_handling          , l_trans_obj.edi_transaction_handling      )
         AND   SELF.equal_varchar2  (SELF.edi_payment_method                , l_trans_obj.edi_payment_method            )
         AND   SELF.equal_varchar2  (SELF.edi_payment_format                , l_trans_obj.edi_payment_format            )
         AND   SELF.equal_varchar2  (SELF.edi_remittance_method             , l_trans_obj.edi_remittance_method         )
         AND   SELF.equal_varchar2  (SELF.edi_remittance_instruction        , l_trans_obj.edi_remittance_instruction    )
         AND   SELF.equal_varchar2  (SELF.bank_charge_bearer                , l_trans_obj.bank_charge_bearer            )
         AND   SELF.equal_varchar2  (SELF.bank_branch_type                  , l_trans_obj.bank_branch_type              )
         AND   SELF.equal_varchar2  (SELF.match_option                      , l_trans_obj.match_option                  )
         AND   SELF.equal_number    (SELF.future_dated_payment_ccid         , l_trans_obj.future_dated_payment_ccid     )
         AND   SELF.equal_varchar2  (SELF.create_debit_memo_flag            , l_trans_obj.create_debit_memo_flag        )
         AND   SELF.equal_varchar2  (SELF.offset_tax_flag                   , l_trans_obj.offset_tax_flag               )
         AND   SELF.equal_number    (SELF.unique_tax_reference_num          , l_trans_obj.unique_tax_reference_num      )
         AND   SELF.equal_number    (SELF.partnership_utr                   , l_trans_obj.partnership_utr               )
         AND   SELF.equal_varchar2  (SELF.partnership_name                  , l_trans_obj.partnership_name              )
         AND   SELF.equal_varchar2  (SELF.cis_enabled_flag                  , l_trans_obj.cis_enabled_flag              )
         AND   SELF.equal_varchar2  (SELF.first_name                        , l_trans_obj.first_name                    )
         AND   SELF.equal_varchar2  (SELF.second_name                       , l_trans_obj.second_name                   )
         AND   SELF.equal_varchar2  (SELF.last_name                         , l_trans_obj.last_name                     )
         AND   SELF.equal_varchar2  (SELF.salutation                        , l_trans_obj.salutation                    )
         AND   SELF.equal_varchar2  (SELF.trading_name                      , l_trans_obj.trading_name                  )
         AND   SELF.equal_varchar2  (SELF.work_reference                    , l_trans_obj.work_reference                )
         AND   SELF.equal_varchar2  (SELF.company_registration_number       , l_trans_obj.company_registration_number   )
         AND   SELF.equal_varchar2  (SELF.national_insurance_number         , l_trans_obj.national_insurance_number     )
         AND   SELF.equal_varchar2  (SELF.verification_number               , l_trans_obj.verification_number           )
         AND   SELF.equal_number    (SELF.verification_request_id           , l_trans_obj.verification_request_id       )
         AND   SELF.equal_varchar2  (SELF.match_status_flag                 , l_trans_obj.match_status_flag             )
         AND   SELF.equal_varchar2  (SELF.cis_verification_date             , l_trans_obj.cis_verification_date         )
         AND   SELF.equal_varchar2  (SELF.individual_1099                   , l_trans_obj.individual_1099               )
         AND   SELF.equal_number    (SELF.cis_parent_vendor_id              , l_trans_obj.cis_parent_vendor_id          )
         AND   SELF.equal_varchar2  (SELF.bus_class_last_certified_date     , l_trans_obj.bus_class_last_certified_date )
         AND   SELF.equal_number    (SELF.bus_class_last_certified_by       , l_trans_obj.bus_class_last_certified_by   )

         AND   SELF.has_same_state_flexible(p_base_object) -- check attributes defined on base
        );

    END has_same_state;

END;
/
