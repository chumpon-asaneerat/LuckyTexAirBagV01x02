# LAB_GREIGESTOCKSTATUS

**Procedure Number**: 305 | **Module**: M14 - LAB (MES Module) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get greige sample stock status and test results |
| **Operation** | SELECT |
| **Tables** | tblLabGreigeStock, tblLabGreigeTest (joined - assumed) |
| **Called From** | LABDataService.cs → LAB_GREIGESTOCKSTATUS() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMERROLL` | VARCHAR2(50) | ⬜ | Beam roll number filter (optional) |
| `P_LOOMNO` | VARCHAR2(50) | ⬜ | Loom machine number filter (optional) |
| `P_RECEIVEDATE` | VARCHAR2(20) | ⬜ | Receive date filter (optional) |

### Output (OUT)

No output parameters - returns cursor

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `BEAMERROLL` | VARCHAR2(50) | Beam roll number |
| `LOOMNO` | VARCHAR2(50) | Loom machine number |
| `ITM_WEAVING` | VARCHAR2(50) | Weaving item code |
| `RECEIVEDATE` | DATE | Lab receive date |
| `RECEIVEBY` | VARCHAR2(50) | Operator who received sample |
| `STATUS` | VARCHAR2(10) | Sample status (RECEIVED/TESTING/TESTED) |
| `CONDITIONINGTIME` | NUMBER | Conditioning time (hours) before testing |
| `TESTDATE` | DATE | Test completion date |
| `TESTRESULT` | VARCHAR2(10) | Test result (PASS/FAIL) |
| `REMARK` | VARCHAR2(500) | Test remarks/notes |
| `TESTBY` | VARCHAR2(50) | Lab technician who tested |
| `APPROVESTATUS` | VARCHAR2(10) | Approval status (APPROVED/REJECTED/PENDING) |
| `APPROVEBY` | VARCHAR2(50) | Lab supervisor who approved |
| `SENDDATE` | DATE | Sample send date to lab |
| `APPROVEDATE` | DATE | Approval date |
| `TESTNO` | NUMBER | Test sequence number |

---

## Business Logic (What it does and why)

Retrieves greige sample inventory status including receive status, test results, and approval workflow. Used for lab workload management and sample tracking.

**Workflow**:
1. Accepts optional filter criteria:
   - Beam roll number
   - Loom number
   - Receive date
2. Queries greige sample stock with complete status
3. Returns sample inventory including:
   - Sample identification
   - Receive and test tracking
   - Test results and approval status
   - Conditioning time (critical for fabric testing)
4. Used for lab management and quality reporting

**Business Rules**:
- All filter parameters optional (can list all greige samples)
- STATUS tracks sample workflow stage
- CONDITIONINGTIME required before testing (fabric must stabilize)
- APPROVESTATUS indicates final disposition

**Sample Status Workflow**:
```
RECEIVED → TESTING → TESTED → APPROVED/REJECTED
   ↓         ↓         ↓           ↓
Receive   Start     Complete   Supervisor
Sample    Test      Test       Decision
```

**Conditioning Time Requirement**:
Greige fabric must be conditioned before testing:
- Standard: 24 hours at controlled temperature/humidity
- Allows fabric to stabilize after weaving
- CONDITIONINGTIME tracked to ensure compliance
- Testing before conditioning = invalid results

**Status Values**:
- **RECEIVED**: Sample in lab, conditioning
- **TESTING**: Currently being tested
- **TESTED**: Test complete, awaiting approval
- **APPROVED/REJECTED**: Final disposition

**Typical Usage**:
```csharp
// Get all samples received today
var today = DateTime.Today.ToString("yyyy-MM-dd");
var todaySamples = LAB_GREIGESTOCKSTATUS(null, null, today);

foreach (var sample in todaySamples)
{
    // Check conditioning time
    if (sample.CONDITIONINGTIME < 24)
    {
        Console.WriteLine($"Sample {sample.BEAMERROLL} - Still conditioning " +
            $"({24 - sample.CONDITIONINGTIME}hrs remaining)");
    }
    else if (sample.STATUS == "RECEIVED")
    {
        Console.WriteLine($"Sample {sample.BEAMERROLL} - Ready for testing");
    }
    else if (sample.STATUS == "TESTED" && sample.APPROVESTATUS == "PENDING")
    {
        Console.WriteLine($"Sample {sample.BEAMERROLL} - Awaiting approval");
    }
}
```

**Lab Workload Dashboard**:
```csharp
// Get pending samples
var allSamples = LAB_GREIGESTOCKSTATUS(null, null, null);

var conditioning = allSamples.Where(s => s.STATUS == "RECEIVED" && s.CONDITIONINGTIME < 24).Count();
var readyToTest = allSamples.Where(s => s.STATUS == "RECEIVED" && s.CONDITIONINGTIME >= 24).Count();
var testing = allSamples.Where(s => s.STATUS == "TESTING").Count();
var pendingApproval = allSamples.Where(s => s.STATUS == "TESTED" && s.APPROVESTATUS == "PENDING").Count();

Console.WriteLine($"Conditioning: {conditioning}");
Console.WriteLine($"Ready to test: {readyToTest}");
Console.WriteLine($"In testing: {testing}");
Console.WriteLine($"Pending approval: {pendingApproval}");
```

**Quality Investigation**:
```csharp
// Find specific beam roll with failed tests
var failedSamples = LAB_GREIGESTOCKSTATUS("BEAM-2025-001", null, null);
var sample = failedSamples.FirstOrDefault();

if (sample != null && sample.TESTRESULT == "FAIL")
{
    Console.WriteLine($"Beam Roll: {sample.BEAMERROLL}");
    Console.WriteLine($"Loom: {sample.LOOMNO}");
    Console.WriteLine($"Test Date: {sample.TESTDATE}");
    Console.WriteLine($"Test By: {sample.TESTBY}");
    Console.WriteLine($"Result: {sample.TESTRESULT}");
    Console.WriteLine($"Remarks: {sample.REMARK}");
    Console.WriteLine($"Approval: {sample.APPROVESTATUS} by {sample.APPROVEBY}");

    // Investigate weaving quality for this loom
    InvestigateLoomQuality(sample.LOOMNO);
}
```

**Approval Workflow**:
```csharp
// Samples awaiting supervisor approval
var pendingSamples = LAB_GREIGESTOCKSTATUS(null, null, null)
    .Where(s => s.STATUS == "TESTED" && s.APPROVESTATUS == "PENDING");

foreach (var sample in pendingSamples)
{
    // Load for supervisor review
    ShowApprovalScreen(sample.BEAMERROLL, sample.LOOMNO,
        sample.TESTRESULT, sample.REMARK);
}
```

---

## Related Procedures

**Related**: [304-LAB_GETWEAVINGSAMPLING.md](./304-LAB_GETWEAVINGSAMPLING.md) - Gets greige sample details
**Related**: LAB_MASSPROSTOCKSTATUS - Similar status for finished fabric samples
**Upstream**: [297-LAB_CHECKRECEIVEGREIGESAMPLING.md](./297-LAB_CHECKRECEIVEGREIGESAMPLING.md) - Checks if sample received
**Downstream**: LAB_UPDATEGREIGESTOCK - Updates sample status

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\LABDataService.cs`
**Method**: `LAB_GREIGESTOCKSTATUS()`
**Lines**: Likely in greige inventory section

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `LAB_GREIGESTOCKSTATUS(LAB_GREIGESTOCKSTATUSParameter para)`
**Lines**: 3991-4020

**Return Structure** (16 columns):
```csharp
public class LAB_GREIGESTOCKSTATUSResult
{
    // Sample identification
    public string BEAMERROLL { get; set; }
    public string LOOMNO { get; set; }
    public string ITM_WEAVING { get; set; }

    // Receive tracking
    public DateTime? RECEIVEDATE { get; set; }
    public DateTime? SENDDATE { get; set; }
    public string RECEIVEBY { get; set; }
    public string STATUS { get; set; }

    // Conditioning & testing
    public decimal? CONDITIONINGTIME { get; set; }  // Hours
    public DateTime? TESTDATE { get; set; }
    public decimal? TESTNO { get; set; }
    public string TESTBY { get; set; }
    public string TESTRESULT { get; set; }          // PASS/FAIL
    public string REMARK { get; set; }

    // Approval
    public string APPROVESTATUS { get; set; }       // APPROVED/REJECTED/PENDING
    public string APPROVEBY { get; set; }
    public DateTime? APPROVEDATE { get; set; }
}
```

**Typical Query Logic**:
```sql
SELECT g.BEAMERROLL, g.LOOMNO, g.ITM_WEAVING,
       g.RECEIVEDATE, g.RECEIVEBY, g.STATUS,
       g.CONDITIONINGTIME, g.SENDDATE,
       t.TESTDATE, t.TESTRESULT, t.TESTBY, t.TESTNO,
       t.REMARK, t.APPROVESTATUS, t.APPROVEBY, t.APPROVEDATE
FROM tblLabGreigeStock g
LEFT JOIN tblLabGreigeTest t ON g.BEAMERROLL = t.BEAMERROLL
                             AND g.LOOMNO = t.LOOMNO
WHERE (:P_BEAMERROLL IS NULL OR g.BEAMERROLL = :P_BEAMERROLL)
  AND (:P_LOOMNO IS NULL OR g.LOOMNO = :P_LOOMNO)
  AND (:P_RECEIVEDATE IS NULL OR TRUNC(g.RECEIVEDATE) = TO_DATE(:P_RECEIVEDATE, 'YYYY-MM-DD'))
ORDER BY g.RECEIVEDATE DESC, g.BEAMERROLL
```

---

**File**: 305/296 | **Progress**: 103.0%
