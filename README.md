# ArduinoLuminosityBalancer
### اقرأ التحديثات اسفل الصفحة 

## نبذة عن المشروع
  هو مشروع يهدف إلى تحقيق التوازن في شدة الإضاءة باستخدام لوحة Arduino. يستجيب المشروع للتغيرات في الإضاءة المحيطة بطريقة ذكية ويعدل شدة الضوء بما يتناسب معها وذلك بإستخدام المعادلة التفاضلية dx_dt = k*(target-x) حيث يوفر طريقة متطورة للتحكم في شدة الضوء في الأنظمة الذكية، مما يتيح توفير إضاءة مريحة ومتكيفة مع البيئة المحيطة بطريقة أكثر طبيعية وفعالية.

## المكونات
  - لوحة Arduino (يُفضل Arduino UNO)
  - مستشعر ضوء (مثل LDR)
  - ديود LED
  - مقاومات وأسلاك توصيل

## كيفية العمل
  يقوم المشروع بقراءة مستويات الضوء المحيطة داخل الغرفة / المكتب عبر مستشعر الضوء ويستخدم هذه المعلومات لتعديل شدة الضوء بحيث يوفر تجربة فريدة للمستخدم من ناحية الاضاءة المريحة. يتم استخدام معادلة تفاضلية معينة لضمان تكيف الإضاءة بطريقة سلسة ودقيقة مع التغيرات البيئية.

## طريقة الاستخدام
  1. قم بتوصيل مستشعر الضوء والديود LED بلوحة Arduino وفقًا للتعليمات المحددة.
  2. قم بتحميل الكود إلى لوحة Arduino.
  3. ضع المشروع في المكان المراد التحكم في إضاءته.
  4. سيقوم المشروع تلقائيًا بتعديل شدة الضوء بناءً على الإضاءة المحيطة.

### المفاهيم الرياضية في `LEDControl.cpp`

#### المعادلة التفاضلية
في ملف `LEDControl.cpp`، تم استخدام معادلة تفاضلية للتحكم في شدة الضوء. المعادلة المستخدمة هي:

x = Target - e^ (-Kt-c)

حيث:
- \( x \): شدة الضوء المطلوبة.
- \( \Target \): القيمة المستهدفة لشدة الضوء، والتي يتم تحديدها بناءً على قراءة الحساس.
- \( k \): ثابت النسبة، والذي يحدد سرعة التغيير في شدة الضوء.
- \( t \): الزمن بالملي ثانية منذ بدء تشغيل البرنامج.
- \( C \): ثابت يعتمد على الفرق بين القراءة الحالية والقراءة السابقة للحساس.

هذه المعادلة تسمح بتغيير تدريجي وسلس لشدة الضوء استجابةً لتغيرات الضوء المحيط.

### المفاهيم الفيزيائية في `SensorReadings.cpp`

#### قراءة مستشعر الضوء
  في ملف `SensorReadings.cpp`، يتم استخدام مستشعر الضوء (مثل LDR - مقاوم ضوئي) لقياس الإضاءة المحيطة. المبدأ الفيزيائي المستخدم هو أن المقاومة الكهربائية للـ LDR تتغير بناءً على مستوى الإضاءة المحيطة به. في الضوء الساطع، تقل مقاومة LDR، وفي الظلام تزداد مقاومته.
  
  - يتم قراءة هذه التغيرات في المقاومة من خلال رجل الإدخال الأنالوج في الأردوينو (مثل A0).
  - يتم حساب المتوسط لعدة قراءات للحصول على قراءة أكثر استقرارًا ودقة.

### خلاصة
  المشروع يدمج بين المفاهيم الرياضية والفيزيائية بطريقة تتيح التحكم الذكي في شدة الضوء استجابةً للتغيرات في البيئة المحيطة. المعادلة التفاضلية توفر طريقة سلسة للتحكم في شدة الضوء، بينما يقدم مستشعر الضوء بيانات دقيقة عن البيئة المحيطة لتعديل الإضاءة وفقًا لها .

## مساهمة
  أشجع الجميع على المساهمة في تطوير هذا المشروع! سواء كان ذلك من خلال تحسين الكود، إضافة ميزات جديدة، أو تقديم اقتراحات لتحسينات.
  ## تحديثات
    تم تحديث المشروع ليقوم بإرسال قيم الحساسات التي قام بقياسها في مدة تتراوح ل 10 دقائق الى وحدة طرفية (صفحة ويب مثلا) ليتم عمل حسابات معينة لاستنتاج الtarget value وارسالها للاردوينو ليقوم بمعالجة الاضاءة بحيث تتكيف مع القيمة السابقة تاريخ التحديث 18/11/2023
