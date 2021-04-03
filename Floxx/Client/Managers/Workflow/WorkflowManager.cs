using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Floxx.Client.Extensions;
using Floxx.Client.Interfaces;
using Floxx.Client.Services;
using Floxx.Shared.Interfaces;
using Floxx.Web.Shared.DTO.Workflows;

namespace Floxx.Client.Managers.Workflow
{
    public class WorkflowManager : IWorkflowManager
    {
        private readonly IApiService apiService;
        private readonly HttpClient _httpClient;


        public WorkflowManager(IApiService apiService, HttpClient httpClient)
        {
            this.apiService = apiService;
            _httpClient = httpClient;
        }

        public async Task<IResult<WorkflowResponse>> CreateAsync(NewWorkflowRequest request)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(Routes.WorkflowEndpoint.Save, request);

            return await response.ToResult<WorkflowResponse>();

            //WorkflowResponse response = await apiService.PostJsonAsync<NewWorkflowRequest, WorkflowResponse>(Routes.WorkflowEndpoint.Save, request);

            //return response;
        }
         
        public async Task<IResult<List<WorkflowResponse>>> GetAllAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(Routes.WorkflowEndpoint.GetAll);

            return await response.ToResult<List<WorkflowResponse>>();
        }

        public async Task<IResult<WorkflowResponse>> GetByIdAsync(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(Routes.WorkflowEndpoint.Get(id));

            return await response.ToResult<WorkflowResponse>();
        }


        public async Task<IResult<WorkflowResponse>> UpdateAsync(UpdateWorkflowRequest request)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync(Routes.WorkflowEndpoint.Save, request);

            return await response.ToResult<WorkflowResponse>();
        }

        public async Task<string> DeleteAsync(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync(Routes.WorkflowEndpoint.Delete(id));

            return await response.Content.ReadAsStringAsync();
        }
    }
}
