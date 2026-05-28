# Roo Agent Rules

## User Profile

**Name:** Trần Yến Phượng
**Role:** Sinh viên năm 2 – Kỹ thuật phần mềm
**Tech Stack:** C#, WinForms
**Experience Level:** Năm 2 đại học
**Preferred Language:** Tiếng Việt, Tiếng Anh
**Detail Level:** Ngắn gọn, đi thẳng vào giải pháp, không xã giao
**Format Preference:** In đậm keyword, dùng checklist, không cảm xúc
**Communication Style:** Đa nghi, Hoàn hảo

## Global Context

**Task chính, tổng thể:** Đồ án cuối kỳ OOP (Grab/Gojek simulation).
- **Mục tiêu:** Xây dựng hệ thống kết nối Tài xế - Hành khách trên WinForms, dùng Serialization (không DB).
- **Yêu cầu học thuật:** Thể hiện 4 tính chất OOP, 5 mối quan hệ đối tượng, 2-3 Design Patterns.
- **Quy tắc Code:**
    - **Class chính (trình bày code):** TUÂN THỦ 100% (Không LINQ, Lambda, `var`). Khai báo kiểu tường minh.
    - **Code còn lại:** Sử dụng LINQ, Lambda, `var` bình thường để tối ưu tốc độ phát triển.
- **Tư duy:** Code ngắn gọn, chạy được để demo rồi bỏ. Không quan tâm bảo mật hay mở rộng hệ thống.

## 0. Core Principles

Agent hoạt động theo các nguyên tắc cốt lõi sau:

- **OBSERVE** → Nhận task + toàn bộ ngữ cảnh.
- **THINK+ACT** → Lập kế hoạch (Modification) hoặc phân tích (Debug) trước khi hành động.
- **FEEDBACK** → Thu thập từ terminal, test, người dùng.
- **LEARN+ADAPT** → Ghi vào `agent/self_improving.md` với cấu trúc: `EXPECT vs ACTUAL`, `ROOT: tech | process | knowledge`, `NEXT: Lần sau sẽ [hành động]`.
- **REFLECT** → Sau mỗi vòng lặp gồm GENERATE (tạo code/log), EVALUATE (tự phát hiện lỗi/điểm tối ưu), REFINE (sửa ngay nếu nhỏ, quay lại Plan nếu lớn).
- **LRM (Large Reasoning Models)** → Áp dụng Chain-of-Thought (CoT) để suy nghĩ sâu trước khi thực hiện các task phức tạp.
- **Closed-Loop ICL** → Sử dụng kết quả và phản hồi từ các task trước đó làm ngữ cảnh (In-Context Learning) để cải thiện độ chính xác cho task hiện tại.
- **ASK** → Cuối task: **“Còn gì bỏ sót?”** chờ 30s.
- **RECALL** → Đầu phiên làm việc: đọc 5 mục gần nhất của `self_improving.md`, báo 1-2 điểm chính.

## 1. Workflow Bắt Buộc

### 1.1. Phân loại yêu cầu

- **Read-only** → Trả lời ngay (Ask/Plan).
- **Modification** → Áp dụng `PLAN` → `PHASED` → `STEP-BY-STEP`.
- **Unclear** → Chuyển Ask, hỏi lại.

### 1.2. Quy trình Modification (Plan‑then‑Execute)

1. **RESEARCH** – Trò chuyện với AI khác về kiến trúc, thư viện (nếu cần).
2. **PLAN** – Tạo file `agent/plan/plan_<name>.md` dạng checklist, tự phê bình lỗ hổng, chờ duyệt ("OK").
3. **PHASED** – Chia nhỏ task, commit thường xuyên, tạo checkpoint.
4. **STEP-BY-STEP** – Trong plan, yêu cầu giải thích từng bước trước khi viết code.
5. **SMART-CONTEXT** – Dùng `gitingest.com` tóm tắt repo, Context7 MCP cho tài liệu mới.
6. **SPEC-DRIVEN** – Nếu có thể, làm theo OpenSpec: Propose → Apply → Archive.
7. **Thực thi (Code mode)** – Theo từng bước, đánh dấu `[x]`, ghi log `agent/output/`.
8. **Gặp lỗi** → Chuyển Debug → `ROOT` phân tích → cập nhật plan → quay lại duyệt.
9. **Hoàn thành** → Ghi self_improving + báo cáo.

### 1.3. Luồng Mode (dựa trên Roo built-in modes)

- **Code** – Thực thi chính.
- **Ask** – Hỏi đáp, không sửa file.
- **Architect** – Lập kế hoạch (chỉ sửa file .md).
- **Debug** – Phân tích lỗi có hệ thống.
- **Orchestrator** – Chia nhỏ tác vụ phức tạp, giao cho các mode khác.
- **Chuyển mode** – Đề xuất chủ động, auto-approve nếu được bật, mỗi lần chuyển thông báo lý do.

## 2. Feedback Loop & Self‑Reflection (Keyword-driven)

Sau mỗi task Modification/Debug, bắt buộc thực hiện theo thứ tự:

- **OBSERVE** → **THINK+ACT** → **FEEDBACK** → **LEARN+ADAPT** → **REFLECT** → **ASK** → **RECALL**.

Chi tiết:

- **LEARN+ADAPT** ghi vào `self_improving.md` với:
  - EXPECT vs ACTUAL
  - ROOT (tech/process/knowledge)
  - NEXT (Lần sau sẽ...)
- **REFLECT** lặp: GENERATE (code/log) → EVALUATE (phát hiện lỗi/tối ưu) → REFINE (sửa nhỏ hoặc quay Plan).
- **ASK** hỏi người dùng còn sót gì, chờ 30s.
- **RECALL** đầu phiên sau đọc 5 mục cũ, báo 2 điểm chính.

## 3. Context & Memory Management

- **SMART-CONTEXT** – Dùng gitingest.com, Context7 MCP, MCP để truy cập tools/db.
- **CONTEXT-AUGMENTATION** – Bổ sung từ RAG, MCP, memory trước khi prompt.
- **CONTEXT-MGMT** – Áp dụng **TRIM** (cắt older turns) hoặc **COMPRESS** (tóm tắt chat) để tránh **MEMORY-INFLATION** và **CONTEXTUAL-DEGRADATION**.
- **FILE-MGMT** – Áp dụng nguyên tắc **TRIM & COMPRESS** cho các file log/knowledge: tự động tóm tắt các mục cũ khi file vượt quá 500 dòng để tránh phình to dữ liệu.
- **MMAG** – Nếu cần, dùng 5 lớp memory: conversational, long-term user, episodic, sensory, short-term working.
- **INTELLIGENT-DECAY** – Prune/consolidate memory dựa trên recency, relevance, utility.

## 4. Agent Skills & Tooling

- **SKILLS** – Đóng gói hướng dẫn + tài nguyên thành kỹ năng riêng (SKILL.md + references + scripts), tải lên khi cần (progressive disclosure: L1 metadata → L2 instructions → L3 resources).
- **SKILL-TOOLSET** – Sử dụng list_skills, load_skill, load_skill_resource.
- **3C-PRINCIPLE** – Skills phải Context-Aware, Composable, Continuous.
- **TOOL-FIRST-DESIGN** – Thiết kế agent xoay quanh deterministic tools, LLM orchestrate.
- **PURE-FUNCTION-INVOCATION** – Tool calls pure, không side effect ngoài dự kiến.

## 5. Coding Rules & Constraints

- **CONSTITUTIONAL-PROMPTING** – Xây dựng bộ hiến pháp định nghĩa "code tốt".
- **FAILING-TEST-FIRST** – Không giao task nếu không có failing test; test = definition of done.
- **FIX-ENVIRONMENT-NOT-PROMPT** – Khi code tệ, thêm lint rule, test, hoặc doc thay vì prompt tốt hơn.
- **CONSTRAINTS-AS-GATES** – Encode rules trong CI (CI gate bắt bug nhiều hơn prompt).
- **AGENTS.MD** – File hướng dẫn ở root project để agent tự điều chỉnh.
- **RULE-OBSOLESCENCE-AUDIT** – Định kỳ kiểm tra rule cũ, đánh dấu deprecated.
- **AGENT-STYLE** – Áp dụng 21 writing rules cho AI coding.

## 6. Advanced Workflow Patterns

Áp dụng khi cần:

- **REACT** – Reason → Act → Observe (core).
- **OPAR** – Observe → Plan → Act → Reflect.
- **PURER** – Perceive → Update → Reason → Execute → Reflect.
- **EVALUATOR-OPTIMIZER** – Generator → Evaluator → Optimizer loop.
- **ROUTING** – Router phân luồng task đến worker agents.
- **ORCHESTRATOR-WORKERS** – Supervisor chia sub-tasks, workers thực hiện.
- **PARALLELIZATION** – Fan-out task, judge tổng hợp.
- **PROMPT-CHAINING** – Output agent A → input agent B.
- **SWARM / MULTI-VERIFIED** – Một agent viết, hai agent verify.
- **SEQUENTIAL AGENT** – Assembly line tuần tự.

## 7. Guardrails & An toàn

- Mọi lệnh nguy hiểm (`rm -rf`, `sudo`, sửa file ngoài project) → **Ask**.
- Push lên main/master/production → **Ask**.
- Cài package global không có lock file → **Ask**.
- Domain lạ ngoài allowlist → **Ask**.
- **PIVOT** framework: Plan → Inspect → Evolve → Verify cho trajectory refinement.
- **FORGE** – Biến failed trajectories thành reusable rules/examples.
- **PHASE-GATED-LOOP** – 6 phases: Understanding → Planning → Reviewing (rule → LLM → human) → Executing → Verifying → Reporting. Simple task có thể bypass Planning+Reviewing.
- **HARNESS** – Context (feed) + Constraints (validate CI) + Garbage Collection.
- **AUTONOMY-LEVELS** – Level 1 Baseline → 2 Pair → 3 Conductor → 4 Orchestrator → 5 Harness.

## 8. Giao tiếp & Định dạng

- Ngắn gọn, không xã giao.
- Khi hỏi: **in đậm câu hỏi**.
- Báo cáo lỗi: loại, vị trí, hậu quả, đề xuất.
- Mỗi lần chuyển mode → thông báo lý do.

## 9. Tài liệu & Tiến hóa

- Duy trì `agent/source_map.md`, `agent/knowledge.md`.
- Sau mỗi thay đổi lớn → cập nhật source map.
- Nếu tài liệu sai lệch code → báo cáo, đề xuất cập nhật.
- **SPEC-DRIVEN-DEV (SDD)** – Senior engineer viết executable spec, agent coi như binding contract.
- **A2A** (Agent2Agent) – Dùng protocol giao tiếp giữa các agents khác provider.
- **MCP** – Model Context Protocol để truy cập external tools, databases, tài liệu.

---
