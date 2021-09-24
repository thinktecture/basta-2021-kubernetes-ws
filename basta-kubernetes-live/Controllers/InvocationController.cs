using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using k8s;


namespace BASTA.Kubernetes.Controllers
{
    [ApiController]
    [Route("invocations")]
    public class InvocationsController : ControllerBase
    {
        [HttpPost("new/{ns}")]
        public async Task<IActionResult> InvokeAsync([FromRoute]string ns)
        {
            // start onetime job
            var client = new k8s.Kubernetes(k8s.KubernetesClientConfiguration.InClusterConfig());
            var body = new k8s.Models.V1Job {
                ApiVersion = "batch/v1",
                Kind = "Job",
                Metadata = new k8s.Models.V1ObjectMeta{
                    Name = $"job-{System.Guid.NewGuid()}",
                    NamespaceProperty = ns,
                },
                Spec = new k8s.Models.V1JobSpec{
                    TtlSecondsAfterFinished = 100,
                    Template = new k8s.Models.V1PodTemplateSpec {
                        Spec = new k8s.Models.V1PodSpec {
                            RestartPolicy = "Never",
                            ImagePullSecrets = new List<k8s.Models.V1LocalObjectReference> {
                                new k8s.Models.V1LocalObjectReference{
                                    Name = "acr"
                                }
                            },
                            Containers = new List<k8s.Models.V1Container> {
                                new k8s.Models.V1Container {
                                    Name = "main",
                                    Image = "bastaws.azurecr.io/job:0.0.1"
                                }
                            }
                        }
                    }
                } 
            };
            var res = await client.CreateNamespacedJobAsync(body, ns);
            return Created("", res);
        }
    }
}
